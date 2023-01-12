using FluentValidation;
using PolizaSOAT.Core.DTOs;

namespace PolizaSOAT.Infrastructure.Validators
{
    public class PolicyValidator: AbstractValidator<PolicyDTO>
    {
        public PolicyValidator()
        {
            RuleFor(policy=> policy.StartDate)
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(policy => policy.FinalDate)
                .GreaterThan(DateTime.Now);
            
            RuleFor(policy => policy.PolicyEndDate)
                .GreaterThanOrEqualTo(policy=>policy.FinalDate);

            RuleFor(policy => policy.VehiclePlate)
                .NotEmpty()
                .MaximumLength(50);


        }
    }
}
