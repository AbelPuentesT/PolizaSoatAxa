using FluentValidation;
using PolizaSOAT.Core.DTOs;

namespace PolizaSOAT.Infrastructure.Validators
{
    public class SecurityValidator : AbstractValidator<SecurityDTO>
    {
        public SecurityValidator()
        {
            RuleFor(security=>security.User)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(security => security.UserName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(security=>security.Password)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(security => security.Rol)
                .IsInEnum();

        }
    }
}
