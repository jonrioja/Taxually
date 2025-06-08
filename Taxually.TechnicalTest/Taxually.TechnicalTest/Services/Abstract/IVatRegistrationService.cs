using Taxually.TechnicalTest.Dtos;

namespace Taxually.TechnicalTest.Services.Abstract;

public interface IVatRegistrationService
{
    Task VatRegisterAsync(VatRegistrationRequest request);
}
