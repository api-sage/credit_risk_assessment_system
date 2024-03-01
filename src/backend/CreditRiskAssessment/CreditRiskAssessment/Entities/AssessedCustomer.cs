using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Entities;

public class AssessedCustomer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime AssessedDate { get; set; }
    public string Name { get; set; }
    [Key]
    [StringLength(11)]
    public string BVN { get; set; }
    public int Age { get; set; }
    public string Occupation { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal MonthlyNetSalary { get; set; }
    public decimal InterestRate { get; set; }
    public int NumberOfLoan { get; set; }
    public int NumberOfDelayedPayment { get; set; }
    public decimal OutstandingDebt { get; set; }
    public decimal DebtToIncomeRatio { get; set; }
    public int MonthsOfCreditHistory { get; set; }
    public decimal PaymentOfMinimumAmount { get; set; }
    public decimal MonthlyInstallmentAmount { get; set; }
    public decimal AmountInvestedMonthly { get; set; }
    public decimal MonthlyBalance { get; set; }
    public int PredictedCreditScore { get; set; }
    public string CreditRating { get; set; }
}
