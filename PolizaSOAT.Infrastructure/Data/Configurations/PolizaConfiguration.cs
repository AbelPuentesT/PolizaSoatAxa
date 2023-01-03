using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class PolizaConfiguration : IEntityTypeConfiguration<Poliza>
    {
        public void Configure(EntityTypeBuilder<Poliza> builder)
        {
            builder.ToTable("Poliza");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("pol_id");

            builder.Property(e => e.CiuId).HasColumnName("ciu_id");

            builder.Property(e => e.CliId).HasColumnName("cli_id");

            builder.Property(e => e.PolFechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_fin");

            builder.Property(e => e.PolFechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_inicio");

            builder.Property(e => e.PolFechaVencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("pol_fecha_vencimiento");

            builder.Property(e => e.PolPlacaAutomotor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pol_placa_automotor")
                    
                    ;

            builder.HasOne(d => d.Ciu).WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.CiuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poliza_Ciudad");

            builder.HasOne(d => d.Cli).WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.CliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Poliza_Cliente");
        }
    }
}
