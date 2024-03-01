using CreditRiskAssessment.AppDbContext;
using CreditRiskAssessment.Entities;
using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Models;
using CreditRiskAssessment.Models.Request;
using CreditRiskAssessment.Models.Response;
using CreditRiskAssessment.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.Design.Serialization;

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
    //TAKES LOAN APPLICANT'S REQUEST AND SENDS IT TO THE ASSESSMENT ENGINE FOR ASSESSMENT
    public async Task<ResponseResult<AssessRiskLevelResponse>> AssessRiskLevel(AssessRiskLevelRequest request)
    {

        //INITIALIZES RESPONSE FRAMEWORK
        var response = new ResponseResult<AssessRiskLevelResponse>()
        {
            status = Constants.FAIL,
            message = string.Empty,
            data = new AssessRiskLevelResponse()
        };

        try
        {
            _logger.Information($"Request :: {JsonConvert.SerializeObject(request)}");

            //THIS MAPS REQUEST FROM USER (LOAN APPLICANT) TO REQUEST TYPE THE ML MODEL EXPECTS
            var modelRequest = new LoanApplicantRequest
            {
                LoanAmount = request.LoanAmount, 
                AnnualIncome = request.AnnualIncome, 
                MonthlyNetSalary = request.MonthlyNetSalary, 
                InterestRate = request.InterestRate, 
                NumberOfLoan = request.NumberOfLoan, 
                NumberOfDelayedPayment = request.NumberOfDelayedPayment, 
                OutstandingDebt = request.OutstandingDebt, 
                DebtToIncomeRatio = request.DebtToIncomeRatio, 
                MonthsOfCreditHistory = request.MonthsOfCreditHistory, 
                PaymentOfMinimumAmount = request.PaymentOfMinimumAmount, 
                MonthlyInstallmentAmount = request.MonthlyInstallmentAmount, 
                AmountInvestedMonthly = request.AmountInvestedMonthly, 
                MonthlyBalance = request.MonthlyBalance
            };

            _logger.Information($"Model Request to Assessment Engine :: {JsonConvert.SerializeObject(modelRequest)}");

            //RESPONSE FROM THE CREDIT ASSESSMENT ENGINE
            var assessCreditWorthiness = _crasService.CalculateCreditScore(modelRequest).Result;

            response.data.PredictedCreditScore = (int)Math.Round(assessCreditWorthiness.data.PredictedCreditScore);
            //USE SWITCH EXPRESSION TO DETERMINE RISK LEVEL
            response.data.CreditRating = (int)assessCreditWorthiness.data.PredictedCreditScore 
                switch
                    {
                        >= 720 => "A",
                        < 720 and >= 690 => "B",
                        < 690 and >= 630 => "C",
                        < 630 and >= 300 => "D",
                        _ => "F"
                    };

            response.message = assessCreditWorthiness.message;
            response.status = assessCreditWorthiness.status;
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

    //THIS METHOD READS THE FIRST 100 ROWS ON TEH CSV FILE AND PERSISTS DATA ACCORDINGLY ON THE CUSTOMERS TABLE ON THE SQL DB
    private void PersistCSVDataOnCustomersTable()
    {
        var csvDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
        var csvfile = Path.Combine(csvDirectory, "ReadFile.csv");

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
