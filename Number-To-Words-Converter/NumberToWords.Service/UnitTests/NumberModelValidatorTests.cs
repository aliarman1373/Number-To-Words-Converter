using FluentValidation.TestHelper;
using NumberToWordsConverter.API.Validators;
using NumberToWordsConverter.Models;

namespace UnitTests
{
    public class NumberModelValidatorTests
    {
        private readonly NumberModelValidator _validator=new NumberModelValidator();

        [Fact]
        public async Task ShouldFailValidation_WhenNumberIsZero()
        {
            var model = CreateModel(0);
            var validationResult =await _validator.TestValidateAsync(model);
            validationResult.ShouldHaveValidationErrorFor(x => x.Number);

        }
        [Fact]
        public async Task ShouldFailValidation_WhenNumberIsNegative()
        {
            var model = CreateModel(-1243);
            var validationResult = await _validator.TestValidateAsync(model);
            validationResult.ShouldHaveValidationErrorFor(x => x.Number);

        }

        [Fact]
        public async Task ShouldFailValidation_WhenIntegerPartIsMoreThan15Digits()
        {
            var model = CreateModel(1234567891234567);
            var validationResult = await _validator.TestValidateAsync(model);
            validationResult.ShouldHaveValidationErrorFor(x => x.Number);

        }
        [Fact]
        public async Task ShouldFailValidation_WhenFractionPartIsMoreThan2Digits()
        {
            var model = CreateModel(123.333m);
            var validationResult = await _validator.TestValidateAsync(model);
            validationResult.ShouldHaveValidationErrorFor(x => x.Number);

        }
        [Fact]
        public async Task ShouldNotFailValidation_WhenModelIsValid()
        {
            var model = CreateModel(123456.25m);
            var validationResult = await _validator.TestValidateAsync(model);
            validationResult.ShouldNotHaveAnyValidationErrors();

        }


        public static NumberModel CreateModel(Decimal number)
        {
            return new NumberModel { Number = number };
        }

    }
}