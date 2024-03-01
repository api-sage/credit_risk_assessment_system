using CreditRiskAssessment.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Models.Request;

public class CheckCreditWorthinessRequest
{
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for business purpose?")]
    public bool Purpose_BusinessLoan { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a house purchase purpose?")]
    public bool Purpose_BuyHouse { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a car purchase purpose?")]
    public bool Purpose_BuyACar { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for debt consolidation?")]
    public bool Purpose_DebtConsolidation { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for educational expenses purpose?")]
    public bool Purpose_EducationalExpenses { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a home improvement purpose?")]
    public bool Purpose_HomeImprovements { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for medical bill settlement purpose?")]
    public bool Purpose_MedicalBills { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for any other purpose aside from the ones described?")]
    public bool Purpose_Other { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a trip purpose?")]
    public bool Purpose_TakeATrip { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a major purchase purpose? E.g: A speculative asset")]
    public bool Purpose_MajorPurchase { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for relocation purpose?")]
    public bool Purpose_Moving { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for renewable energy purpose?")]
    public bool Purpose_RenewableEnergy { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for small business purpose?")]
    public bool Purpose_SmallBusiness { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a vacation purpose?")]
    public bool Purpose_Vacation { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Is this loan for a wedding purpose?")]
    public bool Purpose_Wedding { get; set; }
    [SwaggerSchemaExample("20000")]
    [SwaggerSchema(Description = "What amount do you want to borrow?")]
    public float CurrentLoanAmount { get; set; }
    [SwaggerSchemaExample("5")]
    [SwaggerSchema(Description = "How many months do you want to take this loan for before you make a complete and full pay back with interest?")]
    public float Term { get; set; }
    [SwaggerSchemaExample("400")]
    [SwaggerSchema(Description = "What is your current credit score according to your most recent credit report from the Credit Bureau?")]
    public float CreditScore { get; set; }
    [SwaggerSchemaExample("2")]
    [SwaggerSchema(Description = "How many years have you been on your current job? NB: 0 if unemployed")]
    public float YearsInCurrentJob { get; set; }
    [SwaggerSchemaExample("false")]
    [SwaggerSchema(Description = "Do you legally have a home of yours?")]
    public bool HomeOwnership { get; set; }
    [SwaggerSchemaExample("100000")]
    [SwaggerSchema(Description = "What is your current annual income?")]
    public float AnnualIncome { get; set; }
    [SwaggerSchemaExample("1000")]
    [SwaggerSchema(Description = "What is your current monthly debt?")]
    public float MonthlyDebt { get; set; }
    [SwaggerSchemaExample("5")]
    [SwaggerSchema(Description = "According to your most recent credit report from the Credit Bureau, how many years of credit history do you possess?")]
    public float YearsOfCreditHistory { get; set; }
    [SwaggerSchemaExample("10")]
    [SwaggerSchema(Description = "Today makes it how many months since your last delinquent?")]
    public float MonthsSinceLastDelinquent { get; set; }
    [SwaggerSchema(Description = "How many open accounts do you hold currently?")]
    public float NumberOfOpenAccounts { get; set; }
    [SwaggerSchemaExample("2")]
    [SwaggerSchema(Description = "How many times have you lodged or filed for credit problems?")]
    public float NumberOfCreditProblems { get; set; }
    [SwaggerSchemaExample("0")]
    [SwaggerSchema(Description = "What is your current credit balance?")]
    public float CurrentCreditBalance { get; set; }
    [SwaggerSchemaExample("0")]
    [SwaggerSchema(Description = "What is your maximum open credit?")]
    public float MaximumOpenCredit { get; set; }
    [SwaggerSchemaExample("2")]
    [SwaggerSchema(Description = "How many times have you filed for bankruptcy?")]
    public float Bankruptcies { get; set; }
    [SwaggerSchemaExample("2")]
    [SwaggerSchema(Description = "How many tax liens do you have currently?")]
    public float TaxLiens { get; set; }
    [SwaggerSchemaExample("0.5")]
    [SwaggerSchema(Description = "What is your debt to income ratio?")]
    public float DebitToIncomeRatio { get; set; }
}
