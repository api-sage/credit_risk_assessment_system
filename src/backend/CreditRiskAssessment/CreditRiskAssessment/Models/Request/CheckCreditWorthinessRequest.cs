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
    public float CurrentLoanAmount { get; set; }
    public float Term { get; set; }
    public float CreditScore { get; set; }
    public float YearsInCurrentJob { get; set; }
    public bool HomeOwnership { get; set; } = false;
    public float AnnualIncome { get; set; }
    public float MonthlyDebt { get; set; }
    public float YearsOfCreditHistory { get; set; }
    public float MonthsSinceLastDelinquent { get; set; }
    public float NumberOfOpenAccounts { get; set; }
    public float NumberOfCreditProblems { get; set; }
    public float CurrentCreditBalance { get; set; }
    public float MaximumOpenCredit { get; set; }
    public float Bankruptcies { get; set; }
    public float TaxLiens { get; set; }
    public float DebitToIncomeRatio { get; set; }
}
