using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.ML.Models;
using CreditRiskAssessment.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Interfaces;

public interface ICheckCreditWorthinessService
{
    Task<ResponseResult<LoanApplicantMLResponse>> CheckCreditWorthiness(CheckCreditWorthinessRequest request);
}
