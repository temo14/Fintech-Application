using FluentValidation;

namespace Payment.Application.Features.Transaction.Command.Proccess;

public class ProccessTransactionCommandValidator : AbstractValidator<ProccessTransactionCommand>
{
    public ProccessTransactionCommandValidator()
    {
        RuleFor(e => e.OrderId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(e => e.ExpireDate)
            .GreaterThan(DateTime.Today);

        RuleFor(e => e.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .Must(BeAValidCreditCardNumber).WithMessage("Invalid credit card number.");
    }

    private bool BeAValidCreditCardNumber(string cardNumber)
    {
        if (cardNumber.Length >= 12 && cardNumber.Length <= 19 && cardNumber.All(char.IsDigit))
        {
            int sum = 0;
            bool alternate = false;
            char[] digits = cardNumber.ToCharArray();
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = digits[i] - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }
                sum += digit;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }

        return false;
    }
}