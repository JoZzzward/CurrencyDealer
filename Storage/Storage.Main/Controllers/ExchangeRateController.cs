using Microsoft.AspNetCore.Mvc;
using Storage.Core.Services.ExchangeRates;
using Storage.Core.Services.ExchangeRates.Models;

namespace Storage.Main.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(
        IExchangeRateService exchangeRateService
        )
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet("from/{startDateTime}/to/{endDateTime}")]
    [ProducesResponseType(typeof(IEnumerable<ExchangeRateResponse>), 200)]
    public async Task<IEnumerable<ExchangeRateResponse>> GetExchangeRatesByDatesAsync(
        [FromRoute] DateTime startDateTime,
        [FromRoute] DateTime endDateTime
        )
    {
        var response = await _exchangeRateService.GetExchangeRateValuesBetweenDates(
            startDateTime, 
            endDateTime);

        return response;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExchangeRateResponse>), 200)]
    public async Task<IEnumerable<ExchangeRateResponse>> GetExchangeRatesByDatesAsync([FromQuery] string code)
    {
        var response = await _exchangeRateService.GetExchangeRateValuesByCode(code);

        return response;
    }

}