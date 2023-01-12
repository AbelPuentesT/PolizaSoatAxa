using FluentValidation;
using PolizaSOAT.Core.DTOs;

namespace PolizaSOAT.Infrastructure.Validators
{
    public class CustomerValidator: AbstractValidator<CustomerDTO>
    {
        public CustomerValidator()
        {
            RuleFor(cliente => cliente.IdCustomer)
                .Must(x => long.TryParse(x, out var val) && val > 0)
                .WithMessage("Ingrese un numero de identificacion valido")
                .NotEmpty()
                .Length(7, 50);

            RuleFor(cliente => cliente.FirstLastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Primer Apellido es requerido");

            RuleFor(cliente => cliente.SecondLastName)
                .MaximumLength(50)
                .WithMessage("Apellido muy largo");

            RuleFor(cliente => cliente.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Primer nombre es requerido");

            RuleFor(cliente => cliente.SecondName)
                .MaximumLength(50)
                .WithMessage("Nombre muy largo");

            RuleFor(cliente => cliente.Address)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Direccion de residencia es requerida");

            RuleFor(cliente => cliente.City)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("City de residencia es requerida");

            RuleFor(a => a.Phone)
                .Must(x => long.TryParse(x, out var val) && val > 0)
                .WithMessage("Ingrese un numero de celular valido")
                .MaximumLength(50);

            RuleFor(cliente => cliente.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Ingrese un email valido");

        }
    }
}
