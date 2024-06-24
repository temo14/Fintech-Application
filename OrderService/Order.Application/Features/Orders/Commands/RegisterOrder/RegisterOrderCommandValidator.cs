using FluentValidation;
using Order.Domain.Enums;


namespace Order.Application.Features.Orders.Commands.RegisterOrder;

public class RegisterOrderCommandValidator : AbstractValidator<RegisterOrderCommand>
{
    public RegisterOrderCommandValidator()
    {
        RuleFor(e => e.CompanyId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .LessThanOrEqualTo(decimal.MaxValue);

        RuleFor(e => e.Currency)
            .Must(value => Enum.IsDefined(typeof(Currency), value))
            .WithMessage($"Invalid Currency. Valid values: {GetEnumValuesAndNames<Currency>()}");
    }

    private string GetEnumValuesAndNames<TEnum>() where TEnum : Enum
    {
        var enumValues = Enum.GetValues(typeof(TEnum));
        var enumNames = Enum.GetNames(typeof(TEnum));
        var enumPairs = new string[enumValues.Length];

        for (int i = 0; i < enumValues.Length; i++)
        {
            enumPairs[i] = $"{(int)enumValues.GetValue(i)} - {enumNames[i]}";
        }

        return string.Join(", ", enumPairs);
    }
}