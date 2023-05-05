using FluentValidation;
using NumberToWordsConverter.Models;

namespace NumberToWordsConverter.API.Validators
{
    public class NumberModelValidator : AbstractValidator<NumberModel>
    {
        public NumberModelValidator()
        {
            RuleFor(x => x.Number).Must(IsNumberIntegerPartValid).WithMessage("The integer part for number should be less than 16 digits");
            RuleFor(x => x.Number).Must(IsNumberFractionPartValid).WithMessage("The fraction part for number should be less than 3 digits");
            RuleFor(x => x.Number).GreaterThan(0).WithMessage("The number should be greater than zero");
        }

        private bool IsNumberIntegerPartValid(decimal number)
        {
            var splitedParts = number.ToString().Split('.');
            if (splitedParts[0].Length <= 15) return true;
            return false;
        }

        private bool IsNumberFractionPartValid(decimal number)
        {
            var splitedParts = number.ToString().Split('.');
            if (splitedParts.Length == 2)
            {
                if (splitedParts[1].Length <= 2) return true;
                return false;
            }
            return true;
        }
    }
}
