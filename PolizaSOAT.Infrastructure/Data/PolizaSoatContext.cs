using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using System.Reflection;

namespace PolizaSOAT.Infrastructure.Data;

public partial class PolizaSoatContext : DbContext
{
    public PolizaSoatContext()
    {
    }

    public PolizaSoatContext(DbContextOptions<PolizaSoatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CiudadVenta> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<Security> Securities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
