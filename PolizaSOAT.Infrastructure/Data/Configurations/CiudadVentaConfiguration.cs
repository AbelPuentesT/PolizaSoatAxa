using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class CiudadVentaConfiguration : IEntityTypeConfiguration<CiudadVenta>
    {
        public void Configure(EntityTypeBuilder<CiudadVenta> builder)
        {
            builder.ToTable("CiudadVenta");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.CiuCiudad)
                    .HasColumnName("ciudadVenta")
                    .IsRequired()
                    .HasMaxLength(50);
        }
    }
}