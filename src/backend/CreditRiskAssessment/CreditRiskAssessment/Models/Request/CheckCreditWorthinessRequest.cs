using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Models.Request;

public class CheckCreditWorthinessRequest
{
    public bool Purpose_BusinessLoan { get; set; } = false;
    public bool Purpose_BuyHouse { get; set; } = false;
    public bool Purpose_BuyACar { get; set; } = false;
    public bool Purpose_DebtConsolidation { get; set; } = false;
    public bool Purpose_EducationalExpenses { get; set; } = false;
    public bool Purpose_HomeImprovements { get; set; } = false;
    public bool Purpose_MedicalBills { get; set; } = false;
    public bool Purpose_Other { get; set; } = false;
    public bool Purpose_TakeATrip { get; set; } = false;
    public bool Purpose_MajorPurchase { get; set; } = false;
    public bool Purpose_Moving { get; set; } = false;
    public bool Purpose_RenewableEnergy { get; set; } = false;
    public bool Purpose_SmallBusiness { get; set; } = false;
    public bool Purpose_Vacation { get; set; } = false;
    public bool Purpose_Wedding { get; set; } = false;
    public double CurrentLoanAmount { get; set; }
    public int Term { get; set; }
    public int CreditScore { get; set; }
    public int YearsInCurrentJob { get; set; }
    public bool HomeOwnership { get; set; } = false;
    public double AnnualIncome { get; set; }
    public double MonthlyDebt { get; set; }
    public int YearsOfCreditHistory { get; set; }
    public int MonthsSinceLastDelinquent { get; set; }
    public int NumberOfOpenAccounts { get; set; }
    public int NumberOfCreditProblems { get; set; }
    public double CurrentCreditBalance { get; set; }
    public double MaximumOpenCredit { get; set; }
    public int Bankruptcies { get; set; }
    public int TaxLiens { get; set; }
    public float DebitToIncomeRatio { get; set; }
}
