using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using System.Reflection;

namespace PolizaSOAT.Infrastructure.Data;

public partial class PolicySoatContext : DbContext
{
    public PolicySoatContext()
    {
    }

    public PolicySoatContext(DbContextOptions<PolicySoatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SaleCity> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Security> Securities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
