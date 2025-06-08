using Taxually.TechnicalTest.Validators;
using FluentValidation.TestHelper;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;

namespace Taxually.TechnicalTest.Tests.ValidatorsTests;

public class VatRegistrationRequestValidatorTests
{
    private readonly VatRegistrationRequestValidator _validator = new();

    [Fact]
    public void Request_ShouldHaveError_When_CompanyIsEmpty()
    {
        var model = new VatRegistrationRequest {CompanyId = "1" ,CompanyName = string.Empty, Country = CountryCode.GB };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CompanyName);
    }

    [Fact]
    public void Request_ShouldHaveError_When_CountryNotInEnum()
    {
        var model = new VatRegistrationRequest { CompanyId = "1", CompanyName = "MyCompany", Country = (CountryCode)999 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Country);
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var model = new VatRegistrationRequest
        {
            CompanyName = "MyCompany",
            CompanyId = "1",
            Country = CountryCode.GB
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
