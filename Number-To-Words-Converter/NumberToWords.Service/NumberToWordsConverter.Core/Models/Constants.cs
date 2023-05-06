namespace NumberToWordsConverter.Models
{
    public static class CurrencyUnit
    {
        public static readonly string Dollar = "Dollar";
        public static readonly string Cent = "Cent";
    }

    public static class NumberWords
    {
        public static readonly string[] UnitsMap= { 
                                        "Zero",
                                        "One",
                                        "Two",
                                        "Three",
                                        "Four",
                                        "Five",
                                        "Six",
                                        "Seven",
                                        "Eight",
                                        "Nine",
                                        "Ten",
                                        "Eleven",
                                        "Twelve",
                                        "Thirteen",
                                        "Fourteen",
                                        "Fifteen",
                                        "Sixteen",
                                        "Seventeen",
                                        "Eighteen",
                                        "Ninteen"};

        public static readonly string[] TensMap = { 
                                        "Ten", 
                                        "Twenty", 
                                        "Thirty", 
                                        "Forty", 
                                        "Fifty", 
                                        "Sixty", 
                                        "Seventy", 
                                        "Eighty", 
                                        "Ninety"};

        public static readonly string[] Notations = {
                                        "Hundred",
                                        "Thousand",
                                        "Million",
                                        "Billion",
                                        "Trillion",
                                        "Quadrillion",
                                        "Quintillion",
                                        "Sextillion",
                                        "Septillion",
                                        "Octillion",
                                        "Nonillion"
                                        };
    }
}
