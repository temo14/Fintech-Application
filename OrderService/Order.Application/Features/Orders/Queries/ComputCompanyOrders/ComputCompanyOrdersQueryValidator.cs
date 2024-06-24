using FluentValidation;

namespace Order.Application.Features.Orders.Queries.ComputCompanyOrders;

public class ComputCompanyOrdersQueryValidator : AbstractValidator<ComputCompanyOrdersQuery>
{
    public ComputCompanyOrdersQueryValidator()
    {
        RuleFor(e => e.CompanyId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);
    }
}