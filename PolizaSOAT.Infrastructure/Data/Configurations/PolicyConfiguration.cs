using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Poliza");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("pol_id");

            builder.Property(e => e.IdCity)
                .HasColumnName("ciu_id"); 

            builder.Property(e => e.IdCustomer)
                .HasColumnName("cli_id");

            builder.Property(e => e.FinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_fin");

            builder.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_inicio");

            builder.Property(e => e.PolicyEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_vencimiento");

            builder.Property(e => e.VehiclePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pol_placa_automotor")                  
                    ;

            builder.HasOne(d => d.City).WithMany(p => p.Policies)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poliza_Ciudad");

            builder.HasOne(d => d.Customer).WithMany(p => p.Policies)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poliza_Cliente");
        }
    }
}
