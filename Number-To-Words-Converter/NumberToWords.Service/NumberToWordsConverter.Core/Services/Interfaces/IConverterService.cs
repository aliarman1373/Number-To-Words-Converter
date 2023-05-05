using NumberToWordsConverter.Models;

namespace NumberToWordsConverter.Services.Interfaces
{
    public interface IConverterService
    {
        string ConvertNumberToWords(decimal number);
        List<string> Generate3DigitNumberWords(int number, bool shouldIncludeAnd);
        List<string> GenerateNumberWords(long number);
        List<int> SplitByNotations(long number);
        bool ValidateNumber(decimal number);
        public List<string> GenerateFractionWords(int fractionPart);
        public List<string> GenerateIntegerWords(Int64 integerPart);


    }
}