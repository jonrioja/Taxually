using System.Text;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Strategies.Implementation;

public class FrVatRegistrationStrategy : IVatRegistrationStrategy
{
    public CountryCode CountryCode => CountryCode.FR;
    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
        var bytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        var client = new TaxuallyQueueClient();
        return client.EnqueueAsync("vat-registration-csv", bytes);
    }
}
