using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class SaleCityConfiguration : IEntityTypeConfiguration<SaleCity>
    {
        public void Configure(EntityTypeBuilder<SaleCity> builder)
        {
            builder.ToTable("CiudadVenta");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.City)
                    .HasColumnName("ciudadVenta")
                    .IsRequired()
                    .HasMaxLength(50);
        }
    }
}