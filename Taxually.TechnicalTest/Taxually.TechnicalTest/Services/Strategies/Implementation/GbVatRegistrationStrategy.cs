using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Strategies.Implementation;

public class GbVatRegistrationStrategy : IVatRegistrationStrategy
{
    public CountryCode CountryCode => CountryCode.GB;

    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        var client = new TaxuallyHttpClient();
        return client.PostAsync("https://api.uktax.gov.uk", request);
    }
}
