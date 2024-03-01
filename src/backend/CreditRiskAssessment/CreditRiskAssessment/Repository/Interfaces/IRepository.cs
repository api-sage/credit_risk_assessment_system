using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    T GetCustomerCreditHistoryByBVN(string bvn);
    void AddCustomerCreditHistory(T entity);
    void AddAssessedCreditHistory(T entity);
    T GetAllAssessedCustomerCreditHistoryByBVN(string bvn);
}
