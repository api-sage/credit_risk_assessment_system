using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Models;

public class LoanApplicantMLResponse : LoanApplicantRequest
{
    [ColumnName("LoanStatus")]
    public bool PredictedLoanStatus { get; set; }
    public float Probability { get; set; }
    public float Score { get; set; }
}
