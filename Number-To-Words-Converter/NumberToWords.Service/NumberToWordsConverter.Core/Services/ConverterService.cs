using NumberToWordsConverter.Models;
using NumberToWordsConverter.Services.Interfaces;

using System.Text.RegularExpressions;

namespace NumberToWordsConverter.Services
{
    public class ConverterService : IConverterService
    {
        private readonly Regex NumberRegex = new Regex(@"^\d{1,15}(\.\d{1,2})?$");


        ///<summary>
        ///This function gets a decimal number and a textstyle to provide the number in words by given format. The default text style is Title Case 
        ///</summary>
        public string ConvertNumberToWordsInCurrency(decimal number)
        {
            if (!ValidateNumber(number))
            {

                return null;
            }

            List<string> words = new List<string>();
            var integerPart = Convert.ToInt64(Math.Truncate(number));
            var fractionPart = Convert.ToInt32((number - integerPart) * 100);

            if (integerPart > 0)
            {
                words.AddRange(GenerateNumberWords(integerPart));
                words.Add(CurrencyUnit.Dollar + (integerPart > 1 ? 's' : string.Empty));
                if (fractionPart > 0) words.Add("And");
            }

            if (fractionPart > 0)
            {
                words.AddRange(GenerateNumberWords(fractionPart));
                words.Add(CurrencyUnit.Cent + (fractionPart > 1 ? 's' : string.Empty));
            }

            return String.Join(" ", words);

        }

        ///<summary>
        ///This function gets a decimal number and validate it for converting  
        ///</summary>
        public bool ValidateNumber(decimal number)
        {
            if (NumberRegex.IsMatch(number.ToString()))
            {
                return number > 0;
            }
            return false;
        }

        ///<summary>
        ///This function gets a number without any fraction part and convert the number to the words
        ///</summary>
        public List<string> GenerateNumberWords(Int64 number)
        {
            List<string> words = new List<string>();
            List<int> threeDigits = SplitByNotations(number);

            threeDigits.Reverse();
            bool shouldIncludeAnd = true; ;

            for (int i = 0; i < threeDigits.Count(); i++)
            {

                if (i == threeDigits.Count() - 1)
                {
                    shouldIncludeAnd = false;
                }
                words.InsertRange(0, Generate3DigitNumberWords(threeDigits[i], i, shouldIncludeAnd));

            }

            return words;
        }

        ///<summary>
        ///This function gets a 3-digits number without any fraction part and convert the number to the words
        ///</summary>
        private List<string> Generate3DigitNumberWords(int number, int notationIndex, bool shouldIncludeAnd)
        {

            var threeDigitWord = new List<string>();
            
            if (number > 99 && number < 1000)
            {
                threeDigitWord.Add(NumberWords.UnitsMap[(number / 100)]);
                threeDigitWord.Add(NumberWords.Notations[0]);
                shouldIncludeAnd = true;
                number = number % 100;
            }

            if (number > 0)
            {
                if (shouldIncludeAnd) threeDigitWord.Add("And");
                if (number < 20)
                {
                    threeDigitWord.Add(NumberWords.UnitsMap[number]);
                }
                else
                {
                    var last2Digits = NumberWords.TensMap[(number / 10)-1];
                    if ((number % 10) > 0)
                    {
                        last2Digits += "-" + NumberWords.UnitsMap[number % 10];
                    }
                    threeDigitWord.Add(last2Digits);
                }

            }

            if(notationIndex>0) threeDigitWord.Add(NumberWords.Notations[notationIndex]);
            return threeDigitWord;
        }

        ///<summary>
        ///This function gets a number without any fraction part and chunk the number into 3-digits parts
        ///</summary>
        public List<int> SplitByNotations(Int64 number)
        {
            return String.Format("{0:n0}", number).Split(',').Select(n => Convert.ToInt32(n)).ToList();
        }

    }
}
