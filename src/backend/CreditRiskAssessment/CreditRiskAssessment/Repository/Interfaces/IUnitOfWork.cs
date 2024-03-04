using CreditRiskAssessment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Repository.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Customer> Customers { get; }
    public IRepository<AssessedCustomer> AssessedCustomers { get; }
    void Save();
}
