using System.Xml.Serialization;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Services.Strategies.Implementation;

public class DeVatRegistrationStrategy : IVatRegistrationStrategy
{
    public CountryCode CountryCode => CountryCode.DE;
    public Task VatRegisterAsync(VatRegistrationRequest request)
    {
        var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
        using var writer = new StringWriter();
        serializer.Serialize(writer, request);

        var client = new TaxuallyQueueClient();
        return client.EnqueueAsync("vat-registration-xml", writer.ToString());
    }
}
