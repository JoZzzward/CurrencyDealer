using MassTransit;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Exceptions;

namespace Converter.Core.Services.MassTransit;

public class MassTransitService : IMassTransitService
{
    private readonly IBus _bus;
    private readonly ILogger<MassTransitService> _logger;

    public MassTransitService(
        IBus bus,
        ILogger<MassTransitService> logger
        )
    {
        _bus = bus;
        _logger = logger;
    }


    public async Task PublishData<T>(T data)
    {
        var retriesCount = 15;
        for (int i = 1; i <= retriesCount; i++)
        {
            try
            {
                await _bus.Publish(data!);
                break;
            }
            catch (BrokerUnreachableException)
            {
                _logger.LogWarning("--> Broker unreachable exception.. Attempt {Attempt}", i);
                await Task.Delay(3000);
                continue;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("--> Error: {ErrorMessage}; Attempt {Attempt}", ex.Message, i);
                await Task.Delay(3000);
                continue;
            }
        }
    }
}
