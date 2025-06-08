using FluentValidation;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;

namespace Taxually.TechnicalTest.Validators;

public class VatRegistrationRequestValidator:AbstractValidator<VatRegistrationRequest>
{
    public VatRegistrationRequestValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.CompanyName).NotEmpty();
        RuleFor(x => x.Country)
                  .Must(country =>
                      Enum.IsDefined(typeof(CountryCode), country))
                  .WithMessage(country => $"Country {country.Country} is not supported.");
    }
}
