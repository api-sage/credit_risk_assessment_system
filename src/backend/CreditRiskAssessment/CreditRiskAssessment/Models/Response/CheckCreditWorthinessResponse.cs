using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Models.Response;

public class AssessRiskLevelResponse
{
    public float PredictedCreditScore { get; set; }
    public string RiskLevel { get; set; }
}
