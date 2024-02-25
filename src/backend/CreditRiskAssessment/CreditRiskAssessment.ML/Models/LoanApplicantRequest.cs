using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Models
{
    public class LoanApplicantRequest
    {

        [LoadColumn(0)]
        public float Purpose_BusinessLoan { get; set; }
        [LoadColumn(1)]
        public float Purpose_BuyHouse { get; set; }
        [LoadColumn(2)]
        public float Purpose_BuyACar { get; set; }
        [LoadColumn(3)]
        public float Purpose_DebtConsolidation { get; set; }
        [LoadColumn(4)]
        public float Purpose_EducationalExpenses { get; set; }
        [LoadColumn(5)]
        public float Purpose_HomeImprovements { get; set; }
        [LoadColumn(6)]
        public float Purpose_MedicalBills { get; set; }
        [LoadColumn(7)]
        public float Purpose_Other { get; set; }
        [LoadColumn(8)]
        public float Purpose_TakeATrip { get; set; }
        [LoadColumn(9)]
        public float Purpose_MajorPurchase { get; set; }
        [LoadColumn(10)]
        public float Purpose_Moving { get; set; }
        [LoadColumn(11)]
        public float Purpose_RenewableEnergy { get; set; }
        [LoadColumn(12)]
        public float Purpose_SmallBusiness { get; set; }
        [LoadColumn(13)]
        public float Purpose_Vacation { get; set; }
        [LoadColumn(14)]
        public float Purpose_Wedding { get; set; }
        [ColumnName("Label"), LoadColumn(15)]
        public bool LoanStatus { get; set; }
        [LoadColumn(16)]
        public float CurrentLoanAmount { get; set; }
        [LoadColumn(17)]
        public float Term { get; set; }
        [LoadColumn(18)]
        public float CreditScore { get; set; }
        [LoadColumn(19)]
        public float YearsInCurrentJob { get; set; }
        [LoadColumn(20)]
        public float HomeOwnership { get; set; }
        [LoadColumn(21)]
        public float AnnualIncome { get; set; }
        [LoadColumn(22)]
        public float MonthlyDebt { get; set; }
        [LoadColumn(23)]
        public float YearsOfCreditHistory { get; set; }
        [LoadColumn(24)]
        public float MonthsSinceLastDelinquent { get; set; }
        [LoadColumn(25)]
        public float NumberOfOpenAccounts { get; set; }
        [LoadColumn(26)]
        public float NumberOfCreditProblems { get; set; }
        [LoadColumn(27)]
        public float CurrentCreditBalance { get; set; }
        [LoadColumn(28)]
        public float MaximumOpenCredit { get; set; }
        [LoadColumn(29)]
        public float Bankruptcies { get; set; }
        [LoadColumn(30)]
        public float TaxLiens { get; set; }
        [LoadColumn(31)]
        public float DebitToIncomeRatio { get; set; }
    }
}
