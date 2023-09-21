using Crawler.Core.Extensions;
using Crawler.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Crawler.Core.Services.AppHttpClient.Management;

public class HttpClientServiceManager
{
    private readonly ILogger<HttpClientServiceManager> _logger;
    private readonly HttpClient _httpClient;
    public HttpClientServiceManager(
        HttpClient httpClient, 
        ILogger<HttpClientServiceManager> logger
        )
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    protected async Task<T> GetDeserializedDataFromUrl<T>(string url)
    {
        var data = default(T);

        var retriesCount = 5;
        for (int i = 0; i < retriesCount; i++)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                var content = await response.Content.ReadAsStringAsync();

                content = content.ReplaceCommaOnDot();

                data = XmlHelper.DeserializeFromString<T>(content);

                return data;
            }
            catch (InvalidOperationException ex)
            {
                Task.Delay(3000).Wait();
                _logger.LogError("Invalid operation exception while getting deserialized data from url. Retry: {RetryCount}. Message {ErrorMessage}", i + 1, ex.Message);
            }
            catch (Exception ex)
            {
                Task.Delay(3000).Wait();
                _logger.LogError("Error while getting deserialized data from url. Retry: {RetryCount}. Message: {ErrorMessage}", ex.Message);
            }
        }

        return data;
    }
}