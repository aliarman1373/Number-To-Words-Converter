using Moq;
using NumberToWordsConverter.Models;
using NumberToWordsConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class ConverterServiceTests
    {
        ConverterService _converterService=new ConverterService();

        [Theory]
        [InlineData(1234567891246.52, "One Trillion Two Hundred And Thirty-Four Billion Five Hundred And Sixty-Seven Million Eight Hundred And Ninety-One Thousand Two Hundred And Forty-Six Dollars And Fifty-Two Cents")]
        [InlineData(6543213.25, "Six Million Five Hundred And Forty-Three Thousand Two Hundred And Thirteen Dollars And Twenty-Five Cents")]
        [InlineData(123.30, "One Hundred And Twenty-Three Dollars And Thirty Cents")]
        [InlineData(123.02, "One Hundred And Twenty-Three Dollars And Two Cents")]
        [InlineData(.25, "Twenty-Five Cents")]
        [InlineData(46, "Forty-Six Dollars")]
        [InlineData(.01, "One Cent")]
        [InlineData(1, "One Dollar")]
        [InlineData(000.000, null)]
        [InlineData(000, null)]
        [InlineData(.00021, null)]
        [InlineData(-21321, null)]

        public async Task ConvertNumberToWords_ShouldReturnResult_AsExpected(decimal number, string expected)
        {
            string actual = _converterService.ConvertNumberToWords(number);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(123123.52,true)]
        [InlineData(123456789123456.02,true)]
        [InlineData(9876451235664987,false)]
        [InlineData(0.235,false)]
        [InlineData(0.22,true)]
        [InlineData(1234.256,false)]
        [InlineData(0.0,false)]
        [InlineData(.00,false)]
        [InlineData(0,false)]
        [InlineData(-21321, false)]
        public async Task ValidateNumber_ShouldValidate_AsExpected(decimal number, bool expected)
        {
            bool result=_converterService.ValidateNumber(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123, "One Hundred And Twenty-Three")]
        [InlineData(123.23, "One Hundred And Twenty-Three")]
        [InlineData(-123, "")]
        [InlineData(0, "")]
        [InlineData(0.25, "")]
        [InlineData(12546549684653, "Twelve Trillion Five Hundred And Forty-Six Billion Five Hundred And Forty-Nine Million Six Hundred And Eighty-Four Thousand Six Hundred And Fifty-Three")]
        public async Task GenerateNumberWords_ShouldGenerate_AsExpected(Int64 number, string expected)
        {
            string result = String.Join(" ", _converterService.GenerateNumberWords(number));
            Assert.Equal(expected, result); 
        }

        [Theory]
        [InlineData(123,true, "One Hundred And Twenty-Three")]
        [InlineData(23,false, "Twenty-Three")]
        [InlineData(0,true, "")]
        public async Task Generate3DigitNumberWords_ShouldGenerate_AsExpected(int number,bool shouldIncludeAnd, string expected)
        {
            string result = String.Join(" ", _converterService.Generate3DigitNumberWords(number,shouldIncludeAnd));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(245687986, 3)]
        [InlineData(13323, 2)]
        [InlineData(123, 1)]
        [InlineData(23, 1)]
        [InlineData(0,1)]
        public async Task SplitByNotations_ShouldSplit_AsExpected(Int64 number, int expectedSplitCount)
        {
            int count=_converterService.SplitByNotations(number).Count;
            Assert.Equal(expectedSplitCount, count);
        }

        [Theory]
        [InlineData(123, "One Hundred And Twenty-Three Dollars")]
        [InlineData(1, "One Dollar")]
        public async Task GenerateIntegerWords_ShouldGenerate_AsExpected(Int64 number, string expected)
        {
            string actual = String.Join(" ",_converterService.GenerateIntegerWords(number));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(23, "Twenty-Three Cents")]
        [InlineData(1, "One Cent")]
        public async Task GenerateFractionWords_ShouldGenerate_AsExpected(int number, string expected)
        {
            string actual = String.Join(" ", _converterService.GenerateFractionWords(number));
            Assert.Equal(expected, actual);
        }

    }
}
