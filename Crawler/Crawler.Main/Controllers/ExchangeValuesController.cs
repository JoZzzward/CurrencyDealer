using Crawler.Core.Services.AppHttpClient;
using Crawler.Core.Services.AppHttpClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangeValuesController : ControllerBase
{
    private readonly IHttpClientService _httpClientService;
    public ExchangeValuesController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExchangeValueResponse>), 200)]
    public async Task<IEnumerable<ExchangeValueResponse>> GetExchangeValues([FromQuery] string? date_req = null)
    {
        if (!DateTime.TryParse(date_req, out DateTime dateTime))
            dateTime = DateTime.MinValue;

        var data = await _httpClientService.GetExchangeValueResponsesByDateAsync(dateTime);

        return data;
    }
}
