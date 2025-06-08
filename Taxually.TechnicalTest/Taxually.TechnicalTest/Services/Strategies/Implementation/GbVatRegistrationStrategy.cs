using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Clients.Abstract;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Strategies.Implementation;

public class GbVatRegistrationStrategy : IVatRegistrationStrategy
{
    private readonly ITaxuallyHttpClient _httpClient;

    public GbVatRegistrationStrategy(ITaxuallyHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public CountryCode CountryCode => CountryCode.GB;

    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        return _httpClient.PostAsync("https://api.uktax.gov.uk", request);
    }
}
