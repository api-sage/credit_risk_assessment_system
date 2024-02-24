using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Infrastructure.Commons;

public class ResponseResult<T>
{
    public string? status;
    public string? message;
    public T? data;
}
