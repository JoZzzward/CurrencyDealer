﻿namespace Converter.Core.Services.MassTransit
{
    public interface IMassTransitService
    {
        Task PublishData<T>(T data);
    }
}