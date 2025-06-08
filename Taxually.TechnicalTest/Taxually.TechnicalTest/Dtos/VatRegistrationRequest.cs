using Taxually.TechnicalTest.Enums;

namespace Taxually.TechnicalTest.Dtos;

public class VatRegistrationRequest
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public CountryCode Country { get; set; }
}
