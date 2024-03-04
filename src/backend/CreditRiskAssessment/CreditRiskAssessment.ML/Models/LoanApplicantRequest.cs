using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Models;

//REQUEST THE CRAS.zip MODEL EXPECTS IN THE RIGHT DATA TYPES
public class LoanApplicantRequest
{

    [ColumnName(@"LoanAmount")]
    public float LoanAmount { get; set; }

    [ColumnName(@"AnnualIncome")]
    public float AnnualIncome { get; set; }

    [ColumnName(@"MonthlyNetSalary")]
    public float MonthlyNetSalary { get; set; }

    [ColumnName(@"InterestRate")]
    public float InterestRate { get; set; }

    [ColumnName(@"NumberOfLoan")]
    public float NumberOfLoan { get; set; }

    [ColumnName(@"NumberOfDelayedPayment")]
    public float NumberOfDelayedPayment { get; set; }

    [ColumnName(@"OutstandingDebt")]
    public float OutstandingDebt { get; set; }

    [ColumnName(@"DebtToIncomeRatio")]
    public float DebtToIncomeRatio { get; set; }

    [ColumnName(@"MonthsOfCreditHistory")]
    public float MonthsOfCreditHistory { get; set; }

    [ColumnName(@"PaymentOfMinimumAmount")]
    public float PaymentOfMinimumAmount { get; set; }

    [ColumnName(@"MonthlyInstallmentAmount")]
    public float MonthlyInstallmentAmount { get; set; }

    [ColumnName(@"AmountInvestedMonthly")]
    public float AmountInvestedMonthly { get; set; }

    [ColumnName(@"MonthlyBalance")]
    public float MonthlyBalance { get; set; }

    [ColumnName(@"CreditScore")]
    public float CreditScore { get; set; }
}
