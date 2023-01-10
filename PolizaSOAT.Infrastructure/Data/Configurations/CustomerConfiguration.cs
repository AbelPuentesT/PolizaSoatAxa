using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Enumerations;

namespace PolizaSOAT.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>

    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("cli_id");

            builder.Property(e => e.FirstLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_apellido1");

            builder.Property(e => e.SecondLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_apellido2");

            builder.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_celular");

            builder.Property(e => e.City)
                    .HasColumnName("cli_ciudad")
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasColumnName("cli_direccion");

            builder.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_email");

            builder.Property(e => e.IdCustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_identificacion");

            builder.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_nombre1");

            builder.Property(e => e.SecondName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_nombre2");
        }
    }
}
