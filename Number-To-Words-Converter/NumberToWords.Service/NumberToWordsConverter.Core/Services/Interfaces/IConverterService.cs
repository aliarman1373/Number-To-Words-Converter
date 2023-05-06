using NumberToWordsConverter.Models;

namespace NumberToWordsConverter.Services.Interfaces
{
    public interface IConverterService
    {
        string ConvertNumberToWordsInCurrency(decimal number);
        List<string> GenerateNumberWords(long number);
        List<int> SplitByNotations(long number);
        bool ValidateNumber(decimal number);


    }
}