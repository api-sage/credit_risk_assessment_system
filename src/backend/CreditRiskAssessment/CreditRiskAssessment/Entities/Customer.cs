using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Entities;

public class Customer
{
    public string Name { get; set; }
    [Key]
    [StringLength(11)]
    public string BVN { get; set; }
    public int Age { get; set; }
    public string Occupation { get; set; }
    public double LoanAmount { get; set; }
    public double AnnualIncome { get; set; }
    public double MonthlyNetSalary { get; set; }
    public int InterestRate { get; set; }
    public int NumberOfLoan { get; set; }
    public int NumberOfDelayedPayment { get; set; }
    public double OutstandingDebt { get; set; }
    public int MonthsOfCreditHistory { get; set; }
    public bool PaymentOfMinimumAmount { get; set; }
    public double MonthlyInstallmentAmount { get; set; }
    public double AmountInvestedMonthly { get; set; }
    public double MonthlyBalance { get; set; }
}
