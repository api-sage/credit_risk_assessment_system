using CreditRiskAssessment.AppDbContext;
using CreditRiskAssessment.Entities;
using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Models;
using CreditRiskAssessment.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

namespace CreditRiskAssessment.Services;

public class CheckCreditWorthinessService : ICheckCreditWorthinessService
{
    private ICRAS_Service _crasService;
    private ILogger _logger;
    private readonly CRASDbContext _crasDbContext;
    public CheckCreditWorthinessService(ICRAS_Service crasService, ILogger logger, CRASDbContext crasDbContext)
    {
        _logger = logger;
        _crasService = crasService;
        _crasDbContext = crasDbContext;
    }

    //THIS METHOD ASSESSES CUSTOMER'S CREDIT HISTORY
    public async Task<ResponseResult<AssessRiskLevelResponse>> AssessRiskLevel(string bvn)
    {
        //INITIALIZES RESPONSE FRAMEWORK
        var response = new ResponseResult<AssessRiskLevelResponse>()
        {
            status = Constants.FAIL,
            message = string.Empty,
            data = new AssessRiskLevelResponse()
        };

        if (bvn.Length != 11)
        {
            response.message = "Invalid BVN provided";
            return response;
        };

        try
        {
            PersistCSVDataOnCustomersTable();

            Customer customerCreditHistory = GetCreditHistory(bvn).Result;

            if (customerCreditHistory == null)
            {
                response.message = $"BVN number {bvn} does not exist";
                return response;
            };

            //CALCULATES THE DEBT TO INCOME RATIO IN PERCENTAGE
            var debtToIncome = ((customerCreditHistory.OutstandingDebt * 12)/customerCreditHistory.AnnualIncome)*100;

            //THIS MAPS REQUEST FROM USER(LOAN APPLICANT) TO REQUEST TYPE THE ML MODEL EXPECTS
            var modelRequest = new LoanApplicantRequest
            {
                LoanAmount = (float)customerCreditHistory.LoanAmount,
                AnnualIncome = (float)customerCreditHistory.AnnualIncome,
                MonthlyNetSalary = (float)customerCreditHistory.MonthlyNetSalary,
                InterestRate = (float)customerCreditHistory.InterestRate,
                NumberOfLoan = (float)customerCreditHistory.NumberOfLoan,
                NumberOfDelayedPayment = (float)customerCreditHistory.NumberOfDelayedPayment,
                OutstandingDebt = (float)customerCreditHistory.OutstandingDebt,
                DebtToIncomeRatio = (float)debtToIncome,
                MonthsOfCreditHistory = (float)customerCreditHistory.MonthsOfCreditHistory,
                PaymentOfMinimumAmount = customerCreditHistory.PaymentOfMinimumAmount ? 1 : 0,
                MonthlyInstallmentAmount = (float)customerCreditHistory.MonthlyInstallmentAmount,
                AmountInvestedMonthly = (float)customerCreditHistory.AmountInvestedMonthly,
                MonthlyBalance = (float)customerCreditHistory.MonthlyBalance
            };

            _logger.Information($"Model Request to Assessment Engine :: {JsonConvert.SerializeObject(modelRequest)}");

            //RESPONSE FROM THE CREDIT ASSESSMENT ENGINE
            var predictCreditScore = _crasService.CalculateCreditScore(modelRequest).Result;

            response.data.PredictedCreditScore = (int)Math.Round(predictCreditScore.data.PredictedCreditScore);
            response.data.DebtToIncomeRatio = (int)debtToIncome;
            //USE SWITCH EXPRESSION TO DETERMINE RISK LEVEL
            response.data.CreditRating = (int)predictCreditScore.data.PredictedCreditScore
                switch
            {
                >= 720 => "A",
                < 720 and >= 690 => "B",
                < 690 and >= 630 => "C",
                < 630 and >= 300 => "F",
                _ => "F"
            };
            response.message = "Credit risk assessed successfully";
            response.status = predictCreditScore.status;

            //MAPS PROPERTIES ACCORDINGLY TO ASSESSEDCUSTOMER MODEL TO SAVE ON THE ASSESSED CUSTOMER TABLE
            var assessedCustomer = new AssessedCustomer()
            {
                AssessedDate = DateTime.UtcNow,
                Name = customerCreditHistory.Name,
                BVN = customerCreditHistory.BVN,
                Age = customerCreditHistory.Age,
                Occupation = customerCreditHistory.Occupation,
                LoanAmount = customerCreditHistory.LoanAmount,
                AnnualIncome = customerCreditHistory.AnnualIncome,
                MonthlyNetSalary = customerCreditHistory.MonthlyNetSalary,
                InterestRate = customerCreditHistory.InterestRate,
                NumberOfLoan = customerCreditHistory.NumberOfLoan,
                NumberOfDelayedPayment = customerCreditHistory.NumberOfDelayedPayment,
                OutstandingDebt = customerCreditHistory.OutstandingDebt,
                DebtToIncomeRatio = (float)debtToIncome / 100,
                MonthsOfCreditHistory = customerCreditHistory.MonthsOfCreditHistory,
                PaymentOfMinimumAmount = customerCreditHistory.PaymentOfMinimumAmount,
                MonthlyInstallmentAmount = customerCreditHistory.MonthlyInstallmentAmount,
                AmountInvestedMonthly = customerCreditHistory.AmountInvestedMonthly,
                MonthlyBalance = customerCreditHistory.MonthlyBalance,
                PredictedCreditScore = response.data.PredictedCreditScore,
                CreditRating = response.data.CreditRating
            };

            //SAVES ASSESSED CREDIT HISTORY ON THE DB
            SaveAssessedCreditHistory(assessedCustomer);

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            response.status = Constants.ERROR;
            response.message = ex.Message;
            return response;
        }
    }

    //THIS METHOD FETCHES ASSESSED CUSTOMER CREDIT HISTORY
    public async Task<ResponseResult<List<AssessedCustomer>>> GetAssessedCreditHistory(string request)
    {
        var response = new ResponseResult<List<AssessedCustomer>>()
        {
            status = Constants.FAIL,
            message = string.Empty,
            data = new List<AssessedCustomer>()
        };

        if (request.Length != 11)
        {
            response.message = "Invalid BVN provided";
            return response;
        };
        try
        {
            List<AssessedCustomer> assessdCustomerHistory = _crasDbContext.AssessedCustomers
                .Where(cust => cust.BVN == request).ToList();

            if(assessdCustomerHistory.IsNullOrEmpty())
            {
                response.status = Constants.SUCCESS;
                response.message = "No assessed credit history available for this customer";
                return response;
            };
            response.status = Constants.SUCCESS;
            response.message = "Assessed credit history fetched successfully";
            response.data = assessdCustomerHistory;
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            response.status = Constants.ERROR;
            response.message = ex.Message;
            return response;
        }
    }

    //THIS METHOD PERSISTS ASSESSED CUSTOMERS ON THE ASSESSCUSTOMERS TABLE
    private void SaveAssessedCreditHistory(AssessedCustomer request)
    {
        _crasDbContext.AssessedCustomers.Add(request);
        _crasDbContext.SaveChanges();
    }

    //THIS METHOD FETCHES CUSTOMER'S CREDIT HISTORY USING THE BVN AS THE QUERY PARAMETER
    private async Task<Customer> GetCreditHistory(string bvn)
    {
        return await _crasDbContext.Customers.FirstOrDefaultAsync(c => c.BVN == bvn);
    }

    //THIS METHOD READS THE FIRST 100 ROWS ON TEH CSV FILE AND PERSISTS DATA ACCORDINGLY ON THE CUSTOMERS TABLE ON THE SQL DB
    private void PersistCSVDataOnCustomersTable()
    {
        var csvfile = Path.Combine(Directory.GetCurrentDirectory(), "Helpers","ReadFile.csv");

        if (File.Exists(csvfile))
        {
            var customers = new List<Customer>();

            using (var reader = new StreamReader(csvfile))
            {
                reader.ReadLine();
                while (customers.Count()<51)
                {
                    var line = reader.ReadLine();
                    var dataArray = line!.Split(",");
                    var customer = new Customer();

                    customer.Name = dataArray[0];
                    customer.BVN = dataArray[1];
                    customer.Age = int.Parse(dataArray[2]);
                    customer.Occupation = dataArray[3];
                    customer.LoanAmount = double.Parse(dataArray[4]);
                    customer.AnnualIncome = double.Parse(dataArray[5]);
                    customer.MonthlyNetSalary = double.Parse(dataArray[6]);
                    customer.InterestRate = int.Parse(dataArray[7]);
                    customer.NumberOfLoan = int.Parse(dataArray[8]);
                    customer.NumberOfDelayedPayment = int.Parse(dataArray[9]);
                    customer.OutstandingDebt = double.Parse(dataArray[10]);
                    customer.MonthsOfCreditHistory = int.Parse(dataArray[11]);
                    customer.PaymentOfMinimumAmount = int.Parse(dataArray[12]) == 1;
                    customer.MonthlyInstallmentAmount = double.Parse(dataArray[13]);
                    customer.AmountInvestedMonthly = double.Parse(dataArray[14]);
                    customer.MonthlyBalance = double.Parse(dataArray[15]);

                    if (customers.FirstOrDefault(c => c.BVN == customer.BVN) == null)
                    {
                        customers.Add(customer);
                    }
                }
            }

            foreach (var customerData in customers)
            {
                    _crasDbContext.Customers.Add(customerData);
                    _crasDbContext.SaveChanges();
            }
        }
    }
}
