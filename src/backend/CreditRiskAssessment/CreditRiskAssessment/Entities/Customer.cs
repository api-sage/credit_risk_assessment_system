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
    public int BVN { get; set; }
    public int Age { get; set; }
    public string Occupation { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal MonthlyNetSalary { get; set; }
    public decimal InterestRate { get; set; }
    public int NumberOfLoan { get; set; }
    public int NumberOfDelayedPayment { get; set; }
    public decimal OutstandingDebt { get; set; }
    public int MonthsOfCreditHistory { get; set; }
    public decimal PaymentOfMinimumAmount { get; set; }
    public decimal MonthlyInstallmentAmount { get; set; }
    public decimal AmountInvestedMonthly { get; set; }
    public decimal MonthlyBalance { get; set; }
}
