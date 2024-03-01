using CreditRiskAssessment.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditRiskAssessment.AppDbContext;

public class CRASDbContext : DbContext
{
    public CRASDbContext(DbContextOptions<CRASDbContext> options) : base(options)
    {
    }

    //Creates Customers table on the db
    public DbSet<Customer> Customers { get; set; }
    //Creates AssessedCustomers table on the db
    public DbSet<AssessedCustomer> AssessedCustomers { get; set; }
}
