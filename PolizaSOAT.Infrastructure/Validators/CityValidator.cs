using FluentValidation;
using PolizaSOAT.Core.DTOs;

namespace PolizaSOAT.Infrastructure.Validators
{
    public class CityValidator : AbstractValidator<SaleCityDTO>
    {
        public CityValidator()
        {
            RuleFor(ciudad => ciudad.City)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
