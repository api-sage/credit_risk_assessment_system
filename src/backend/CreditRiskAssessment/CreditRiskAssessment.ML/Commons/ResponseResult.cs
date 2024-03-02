using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Infrastructure.Commons;

public class ResponseResult<T>
{
    public DateTime timeStamp { get => DateTime.UtcNow; }
    public string? status { get; set; }
    public string? message { get; set; }
    public T? data { get; set; }
}
