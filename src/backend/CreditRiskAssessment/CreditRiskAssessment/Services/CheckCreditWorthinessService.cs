using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Models;
using CreditRiskAssessment.Models.Request;
using CreditRiskAssessment.Models.Response;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.Design.Serialization;

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
}
