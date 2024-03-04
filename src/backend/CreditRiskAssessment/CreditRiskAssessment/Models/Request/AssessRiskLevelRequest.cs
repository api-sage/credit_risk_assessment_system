using CreditRiskAssessment.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Models.Request;

public class AssessRiskLevelRequest
{
    public float LoanAmount { get; set; }
    public float AnnualIncome { get; set; }
    public float MonthlyNetSalary { get; set; }
    public float InterestRate { get; set; }
    public float NumberOfLoan { get; set; }
    public float NumberOfDelayedPayment { get; set; }
    public float OutstandingDebt { get; set; }
    public float DebtToIncomeRatio { get; set; }
    public float MonthsOfCreditHistory { get; set; }
    public float PaymentOfMinimumAmount { get; set; }
    public float MonthlyInstallmentAmount { get; set; }
    public float AmountInvestedMonthly { get; set; }
    public float MonthlyBalance { get; set; }
}
