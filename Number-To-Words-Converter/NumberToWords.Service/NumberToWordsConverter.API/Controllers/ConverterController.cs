using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NumberToWordsConverter.Models;
using NumberToWordsConverter.Services.Interfaces;

namespace NumberToWordsConverter.Controllers
{
    public class ConverterController : BaseController
    {
        private readonly IConverterService _converterService;

        public ConverterController(IConverterService converterService)
        {
            _converterService = converterService;
        }


        /// <summary>
        /// Convert a decimal number to words
        /// </summary>
        /// <param name="model">The number model which contais the value of the decimal number</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("ConvertNumberToWords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> GetConvertNumberToWords([FromBody] NumberModel model,CancellationToken cancellationToken)
        {
            return Ok(_converterService.ConvertNumberToWordsInCurrency(Convert.ToDecimal(model.Number)));
        }
    }
}
