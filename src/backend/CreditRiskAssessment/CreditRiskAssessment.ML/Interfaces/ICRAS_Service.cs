using CreditRiskAssessment.ML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Interfaces
{
    public interface ICRAS_Service
    {
        Task<string> TrainModelAsync();
        LoanApplicantMLResponse AssessCreditWorthiness(LoanApplicantRequest request);
    }
}
