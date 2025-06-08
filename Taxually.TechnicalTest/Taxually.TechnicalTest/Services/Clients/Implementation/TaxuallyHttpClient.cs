using Taxually.TechnicalTest.Services.Clients.Abstract;

namespace Taxually.TechnicalTest.Services.Clients.Implementation;

public class TaxuallyHttpClient: ITaxuallyHttpClient
{
    public Task PostAsync<TRequest>(string url, TRequest request)
    {
        // Actual HTTP call removed for purposes of this exercise
        return Task.CompletedTask;
    }
}
