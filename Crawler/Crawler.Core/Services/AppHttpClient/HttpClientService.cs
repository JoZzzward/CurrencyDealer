using Crawler.Core.Services.AppHttpClient.Management;
using Crawler.Core.Services.AppHttpClient.Models;
using Crawler.Core.Services.Hangfire;
using Crawler.Core.Services.MassTransit;
using ExchangeTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml;

namespace Crawler.Core.Services.AppHttpClient;

public class HttpClientService : HttpClientServiceManager, IHttpClientService
{
    private readonly IHangfireService _hangfireService;
    private readonly IMassTransitService _massTransitService;
    private readonly ILogger<HttpClientService> _logger;
    private readonly IConfiguration _configuration;

    private CurrencyHandbookArray HandbookValues = new ();

    public HttpClientService(
        HttpClient httpClient,
        IHangfireService hangfireService,
        IMassTransitService massTransitService,
        ILogger<HttpClientService> logger,
        IConfiguration configuration
        ) : base( 
            httpClient,
            logger
            )
    {
        _hangfireService = hangfireService;
        _logger = logger;
        _configuration = configuration;
        _massTransitService = massTransitService;
    }

    public async Task<IEnumerable<ExchangeValueResponse>> GetExchangeRatesAsync(DateTime limit)
    {
        try
        {
            await GetCurrencyHandbookAsync(false);

            var result = new List<ExchangeValueResponse>();

            var dateTime = _hangfireService.CheckIfDateIsCorrect(limit);
            var currentDate = DateTime.UtcNow;

            while (dateTime.CompareTo(currentDate) < 0)
            {
                var data = await GetExchangeValueResponsesByDateAsync(dateTime);

                var sendData = new ConvertExchangeRateDto() { Items = new ConvertExchangeRateItemDto[data.Count()] };

                result.AddRange(data);

                var arr = data.ToArray();

                for (int i = 0; i < arr.Length; i++)
                {
                    sendData.Items[i] = new ConvertExchangeRateItemDto()
                    {
                        Id = arr[i].Id,
                        Name = arr[i].Name,
                        EngName = arr[i].EngName,
                        Nominal = arr[i].Nominal,
                        ISO_Char_Code = arr[i].ISO_Char_Code,
                        Date = arr[i].Date,
                        Value = arr[i].Value
                    };
                }
                
                await _massTransitService.PublishData(sendData);

                dateTime = dateTime.AddDays(1);
            }

            _logger.LogInformation("Sending of exchange values has been successfully started..");

            return result;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError("Invalid operation exception while getting exchange values. Message {ErrorMessage}", ex.Message);
        }
        catch (XmlException ex)
        {
            _logger.LogError("XML Error at line {ErrorLineNumber}: {ErrorMessage}", ex.LineNumber, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while getting exchange values. Message: {ErrorMessage}", ex.Message);
        }

        return default!;
    }

    public async Task<CurrencyHandbookArray> GetCurrencyHandbookAsync(bool needToBeSended = true)
    {
        try
        {
            HandbookValues = await GetDeserializedDataFromUrl<CurrencyHandbookArray>(
                _configuration["CbrSettings:CurrencyHandbookLink"]!
                ).WaitAsync(new CancellationToken());

            var itemsCounts = HandbookValues.Items.Count;

            var data = new CurrencyHandbookDto() { Items = new CurrencyHandbookDtoItem[itemsCounts] };

            for (int i = 0; i < itemsCounts; i++)
                data.Items[i] = new CurrencyHandbookDtoItem()
                {
                    Id = HandbookValues.Items[i].Id,
                    Name = HandbookValues.Items[i].Name,
                    EngName = HandbookValues.Items[i].EngName,
                    ParentCode = HandbookValues.Items[i].ParentCode,
                    ISOCharCode = HandbookValues.Items[i].ISO_Char_Code
                };

            if (needToBeSended)
                await _massTransitService.PublishData(data);

            _logger.LogInformation("Currency handbook was successfully returned. Currency handbook Items Amount: {HandbookItemsCount}", itemsCounts);

            return HandbookValues;
        }
        catch (XmlException ex)
        {
            _logger.LogError("XML Error while getting currency handbook at line {ErrorLineNumber}: {ErrorMessage}", ex.LineNumber, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while getting currency handbook. Message: {ErrorMessage}", ex.Message);
        }

        return HandbookValues;
    }

    public async Task<IEnumerable<ExchangeValueResponse>> GetExchangeValueResponsesByDateAsync(DateTime dateTime)
    {
        _logger.LogInformation("Trying to get deserialized data from url. DateTime: {DeserializedDateTime}", dateTime.ToShortDateString());
        var values = await GetDeserializedDataFromUrl<ExchangeRates>(
            _configuration["CbrSettings:ExchangeRatesLink"] + dateTime.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))
            );

        _logger.LogInformation("Preparing information..");
        var date = DateTime.ParseExact(values.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);

        var exchangeValues = values.Values
            .Select(ev =>
            {
                var handbook = HandbookValues.Items.FirstOrDefault(hv => hv.Id == ev.Id)
                    ?? new CurrencyHandbookArrayItem();

                return new ExchangeValueResponse
                {
                    Id = ev.Id,
                    Name = ev.Name,
                    EngName = handbook.EngName,
                    ISO_Char_Code = handbook.ISO_Char_Code,
                    Value = ev.Value,
                    Nominal = ev.Nominal,
                    Date = date
                };
            });

        _logger.LogInformation("Deserialized data from url was successfully returned. DateTime: {DeserializedDateTime}", dateTime.ToShortDateString());

        return exchangeValues;
    }
}