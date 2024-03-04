using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.ML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.ML.Interfaces;

public interface ICRAS_Service
{
    Task<ResponseResult<string>> TrainModelAsync();
    Task<ResponseResult<LoanApplicantMLResponse>> CalculateCreditScore(LoanApplicantRequest request);
}
