using System.Text;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Clients.Abstract;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Strategies.Implementation;

public class FrVatRegistrationStrategy : IVatRegistrationStrategy
{
    private readonly ITaxuallyQueueClient _queueClient;


    public FrVatRegistrationStrategy(ITaxuallyQueueClient queueClient)
    {
         _queueClient = queueClient;
    }
    public CountryCode CountryCode => CountryCode.FR;
    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
        var bytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        return _queueClient.EnqueueAsync("vat-registration-csv", bytes);
    }
}
