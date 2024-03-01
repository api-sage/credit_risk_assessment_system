using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Models.Response;

public class AssessRiskLevelResponse
{
    public int DebtToIncomeRatio { get; set; }
    public int PredictedCreditScore { get; set; }
    public string CreditRating { get; set; }
}
