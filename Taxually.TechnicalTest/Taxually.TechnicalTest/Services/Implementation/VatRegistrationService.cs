using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Abstract;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Implementation;

public class VatRegistrationService : IVatRegistrationService
{
    private readonly IDictionary<CountryCode, IVatRegistrationStrategy> _strategies;
    public VatRegistrationService(IEnumerable<IVatRegistrationStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.CountryCode);
    }
    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        var strategy = _strategies[request.Country];
        return strategy.VatRegisterAsync(request);
    }
}
