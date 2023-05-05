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
        public string ConvertNumberToWords(decimal number)
        {
            if (!ValidateNumber(number))
            {

                return null;
            }

            List<string> words = new List<string>();
            var integerPart = Math.Truncate(number);
            var fractionPart = (number - integerPart) * 100;
            if (integerPart > 0)
                words.AddRange(GenerateIntegerWords(Convert.ToInt64(integerPart)));

            if (fractionPart > 0 && integerPart > 0) { words.Add("And"); }

            if (fractionPart > 0)
                words.AddRange(GenerateFractionWords(Convert.ToInt32(fractionPart)));

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
            List<string> temp = new List<string>();

            List<int> threeDigits = SplitByNotations(number);
            threeDigits.Reverse();
            bool shouldIncludeAnd = !(threeDigits.Count() == 1);

            for (int i = 0; i < threeDigits.Count(); i++)
            {

                if (i != 0)
                {
                    temp = Generate3DigitNumberWords(threeDigits[i], !shouldIncludeAnd);
                    temp.Add(NumberWords.Notations[i]);
                    words.InsertRange(0, temp);
                }
                else
                {
                    words.InsertRange(0, Generate3DigitNumberWords(threeDigits[i], shouldIncludeAnd));
                }

            }

            return words;
        }

        ///<summary>
        ///This function gets a 3-digits number without any fraction part and convert the number to the words
        ///</summary>
        public List<string> Generate3DigitNumberWords(int number, bool shouldIncludeAnd)
        {

            List<string> threeDigitWord = new List<string>();
            string last2Digits = "";

            if (number > 99 && number < 1000)
            {
                threeDigitWord.Add(NumberWords.OneToNine[(number / 100) - 1]);
                threeDigitWord.Add(NumberWords.Notations[0]);
                shouldIncludeAnd = true;
                number = number % 100;
            }

            if (number > 19 && number < 100)
            {
                last2Digits = NumberWords.TenToNinety[(number / 10) - 1];
                number = number % 10;
            }

            if (number > 0 && number < 20)
            {
                if (last2Digits != "")
                {
                    last2Digits += "-" + NumberWords.OneToNine[number - 1];
                }
                else
                {
                    last2Digits = NumberWords.OneToNine[number - 1];

                }
            }
            if (last2Digits != "")
            {
                if (shouldIncludeAnd) threeDigitWord.Add("And");
                threeDigitWord.Add(last2Digits);
            }

            return threeDigitWord;
        }


        ///<summary>
        ///This function gets a number without any fraction part and chunk the number into 3-digits parts
        ///</summary>
        public List<int> SplitByNotations(Int64 number)
        {
            return String.Format("{0:n0}", number).Split(',').Select(n => Convert.ToInt32(n)).ToList();
        }

        ///<summary>
        ///This function gets a number and generate number words in dollar 
        ///</summary>
        public List<string> GenerateIntegerWords(Int64 integerPart)
        {
            List<string> words = new List<string>();
            words.AddRange(GenerateNumberWords(integerPart));
            words.Add(CurrencyUnit.Dollar + (integerPart > 1 ? 's' : ""));
            return words;
        }

        ///<summary>
        ///This function gets a number and generate number words in cent
        ///</summary>
        public List<string> GenerateFractionWords(int fractionPart)
        {

            List<string> words = new List<string>();
            words.AddRange(GenerateNumberWords(fractionPart));
            words.Add(CurrencyUnit.Cent + (fractionPart > 1 ? 's' : ""));
            return words;
        }

    }
}
