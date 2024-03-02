using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Models;

//RESPONSE FRAMEWORK THE CRAS MODEL RETURNS
//EQUALLY-NAMED PROPERTIES FROM CRAS RESPONSE GETS MAPPED TO THE PROPETIES IN THIS RESPONSE CLASS
public class LoanApplicantMLResponse
{
    [ColumnName(@"Score")]
    public float PredictedCreditScore { get; set; }
}
