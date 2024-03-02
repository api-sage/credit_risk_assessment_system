using CreditRiskAssessment.AppDbContext;
using CreditRiskAssessment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAssessment.Repository.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CRASDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(CRASDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    public void AddAssessedCreditHistory(T entity)
    {
        dbSet.Add(entity);
    }

    public void AddCustomerCreditHistory(T entity)
    {
        dbSet.Add(entity);
    }

    public T GetAllAssessedCustomerCreditHistoryByBVN(string bvn)
    {
        return dbSet.Find(bvn)!;
    }

    public T GetCustomerCreditHistoryByBVN(string bvn)
    {
        return dbSet.Find(bvn)!;
    }
    public bool Any(Expression<Func<T, bool>> filter)
    {
        return dbSet.Any(filter);
    }
}
