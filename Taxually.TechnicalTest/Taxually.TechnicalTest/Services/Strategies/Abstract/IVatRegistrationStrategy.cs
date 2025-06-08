using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;

namespace Taxually.TechnicalTest.Services.Strategies.Abstract;

public interface IVatRegistrationStrategy
{
    CountryCode CountryCode { get; }
    Task VatRegisterAsync(VatRegistrationRequest request);
}
