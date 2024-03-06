using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.DTO;

public class AssessedCustomerDTO
{
    public int SN { get; set; }
    public string AssessedDate { get; set; }
    public string Name { get; set; }
    public string BVN { get; set; }
    public int Age { get; set; }
    public string Occupation { get; set; }
    public double LoanAmount { get; set; }
    public double AnnualIncome { get; set; }
    public double MonthlyNetSalary { get; set; }
    public double InterestRate { get; set; }
    public int NumberOfLoan { get; set; }
    public int NumberOfDelayedPayment { get; set; }
    public double OutstandingDebt { get; set; }
    public float DebtToIncomeRatio { get; set; }
    public int MonthsOfCreditHistory { get; set; }
    public bool PaymentOfMinimumAmount { get; set; }
    public double MonthlyInstallmentAmount { get; set; }
    public double AmountInvestedMonthly { get; set; }
    public double MonthlyBalance { get; set; }
    public int PredictedCreditScore { get; set; }
    public string CreditRating { get; set; }
    
}
