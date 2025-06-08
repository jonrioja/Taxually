namespace Taxually.TechnicalTest.Services.Clients.Abstract;

public interface ITaxuallyHttpClient
{
    Task PostAsync<TRequest>(string url, TRequest request);
}
