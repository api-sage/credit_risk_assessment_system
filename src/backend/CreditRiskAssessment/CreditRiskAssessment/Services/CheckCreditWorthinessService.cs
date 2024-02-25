using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Models;
using CreditRiskAssessment.Models.Request;
using Newtonsoft.Json;
using Serilog;

namespace CreditRiskAssessment.Services;

public class CheckCreditWorthinessService : ICheckCreditWorthinessService
{
    private ICRAS_Service _crasService;
    private ILogger _logger;
    public CheckCreditWorthinessService(ICRAS_Service crasService, ILogger logger)
    {
        _logger = logger;
        _crasService = crasService;
    }
    //TAKES LOAN APPLICANT'S REQUEST AND SENDS IT TO THE ASSESSMENT ENGINE FOR ASSESSMENT
    public async Task<ResponseResult<LoanApplicantMLResponse>> CheckCreditWorthiness(CheckCreditWorthinessRequest request)
    {
        _logger.Information($"Request :: {JsonConvert.SerializeObject(request)}");
        var response = new ResponseResult<LoanApplicantMLResponse>();

        //THIS MAPS REQUEST FROM USER (LOAN APPLICANT) TO REQUEST TYPE THE ML MODEL EXPECTS
        var modelRequest = new LoanApplicantRequest
        {
            Purpose_BusinessLoan = request.Purpose_BusinessLoan ? 1 : 0,
            Purpose_BuyHouse = request.Purpose_BuyHouse ? 1 : 0,
            Purpose_BuyACar = request.Purpose_BuyACar ? 1 : 0,
            Purpose_DebtConsolidation = request.Purpose_DebtConsolidation ? 1 : 0,
            Purpose_EducationalExpenses = request.Purpose_EducationalExpenses ? 1 : 0,
            Purpose_HomeImprovements = request.Purpose_HomeImprovements ? 1 : 0,
            Purpose_MedicalBills = request.Purpose_MedicalBills ? 1 : 0,
            Purpose_Other = request.Purpose_Other ? 1 : 0,
            Purpose_TakeATrip = request.Purpose_TakeATrip ? 1 : 0,
            Purpose_MajorPurchase = request.Purpose_MajorPurchase ? 1 : 0,
            Purpose_Moving = request.Purpose_Moving ? 1 : 0,
            Purpose_RenewableEnergy = request.Purpose_RenewableEnergy ? 1 : 0,
            Purpose_SmallBusiness = request.Purpose_SmallBusiness ? 1 : 0,
            Purpose_Vacation = request.Purpose_Vacation ? 1 : 0,
            Purpose_Wedding = request.Purpose_Wedding ? 1 : 0,
            LoanStatus = false,
            CurrentLoanAmount = request.CurrentLoanAmount,
            Term = request.Term,
            CreditScore = request.CreditScore,
            YearsInCurrentJob = request.YearsInCurrentJob,
            HomeOwnership = request.HomeOwnership ? 1 : 0,
            AnnualIncome = request.AnnualIncome,
            MonthlyDebt = request.MonthlyDebt,
            YearsOfCreditHistory = request.YearsOfCreditHistory,
            MonthsSinceLastDelinquent = request.MonthsSinceLastDelinquent,
            NumberOfOpenAccounts = request.NumberOfOpenAccounts,
            NumberOfCreditProblems = request.NumberOfCreditProblems,
            CurrentCreditBalance = request.CurrentCreditBalance,
            MaximumOpenCredit = request.MaximumOpenCredit,
            Bankruptcies = request.Bankruptcies,
            TaxLiens = request.TaxLiens,
            DebitToIncomeRatio = request.DebitToIncomeRatio,
        };
        _logger.Information($"Model Request to Assessment Engine :: {JsonConvert.SerializeObject(modelRequest)}");
        //RESPONSE FROM THE CREDIT ASSESSMENT ENGINE
        var assessCreditWorthiness = _crasService.AssessCreditWorthiness(modelRequest);
        _logger.Information($"Credit Worthiness Response :: {JsonConvert.SerializeObject(assessCreditWorthiness)}");
        response.status = Constants.SUCCESS;
        response.data = assessCreditWorthiness;
        response.message = Constants.SUCCESS;
        return response;
    }
}
