using FluentValidation;
using PolizaSOAT.Core.DTOs;

namespace PolizaSOAT.Infrastructure.Validators
{
    public class PolicyValidator: AbstractValidator<PolicyDTO>
    {
        public PolicyValidator()
        {
            RuleFor(poliza => poliza.VehiclePlate)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(poliza => poliza.IdCustomer)
                .NotEmpty()
                .WithMessage("Ingrese una ciudad que se encuentre en la base de datos ");
            
            RuleFor(poliza => poliza.IdCity)
                .NotEmpty()
                .WithMessage("Ingrese un cliente que se encuentre en la base de datos");

        }
    }
}
