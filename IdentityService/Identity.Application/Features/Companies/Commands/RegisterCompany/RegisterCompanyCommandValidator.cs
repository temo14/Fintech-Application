using FluentValidation;
using IdentityService.Application.Features.Companies.Commands.RegisterCompany;

namespace Identity.Application.Features.Companies.Commands.RegisterCompany;

public class RegisterCompanyCommandValidator : AbstractValidator<RegisterCompanyCommand>
{
    public RegisterCompanyCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEqual("string");

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}