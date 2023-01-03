using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>

    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("cli_id");

            builder.Property(e => e.CliApellido1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_apellido1");

            builder.Property(e => e.CliApellido2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_apellido2");

            builder.Property(e => e.CliCelular)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_celular");

            builder.Property(e => e.CliCiudad)
                    .HasColumnName("cli_ciudad")
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.CliDireccion)
                    .IsUnicode(false)
                    .HasColumnName("cli_direccion");

            builder.Property(e => e.CliEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_email");

            builder.Property(e => e.CliIdentificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_identificacion");

            builder.Property(e => e.CliNombre1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_nombre1");

            builder.Property(e => e.CliNombre2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_nombre2");
        }
    }
}
