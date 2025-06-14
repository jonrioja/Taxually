﻿namespace Taxually.TechnicalTest.Services.Clients.Abstract;

public interface ITaxuallyQueueClient
{
    Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}
