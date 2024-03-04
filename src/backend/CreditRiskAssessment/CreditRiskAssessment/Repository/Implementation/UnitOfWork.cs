using CreditRiskAssessment.AppDbContext;
using CreditRiskAssessment.Entities;
using CreditRiskAssessment.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Repository.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly CRASDbContext _db;

    public UnitOfWork(CRASDbContext db)
    {
        _db = db;
    }
    public IRepository<Customer> Customers => new Repository<Customer>(_db);

    public IRepository<AssessedCustomer> AssessedCustomers => new Repository<AssessedCustomer>(_db);

    public void Save()
    {
        _db.SaveChanges();
    }
}
