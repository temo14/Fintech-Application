using FluentValidation;
using IdentityService.Application.Features.Companies.Queries.GetCompanyDetails;

namespace Identity.Application.Features.Companies.Queries.GetCompanyDetails;

public class GetCompanyDetailsQueryValidator : AbstractValidator<GetCompanyDetailsQuery>
{
    public GetCompanyDetailsQueryValidator()
    {
        RuleFor(e => e.Id)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}