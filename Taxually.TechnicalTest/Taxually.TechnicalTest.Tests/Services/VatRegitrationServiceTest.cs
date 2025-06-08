using Moq;
using Taxually.TechnicalTest.Dtos;
using Taxually.TechnicalTest.Enums;
using Taxually.TechnicalTest.Services.Implementation;
using Taxually.TechnicalTest.Services.Strategies.Abstract;

namespace Taxually.TechnicalTest.Tests.Services;

public class VatRegitrationServiceTest
{
    [Theory]
    [InlineData(CountryCode.GB)]
    [InlineData(CountryCode.FR)]
    [InlineData(CountryCode.DE)]
    public async void VatRegistrationService_CallsEachCountrysService(CountryCode countryCode)
    {
        // Arrange
        var strategyMock = new Mock<IVatRegistrationStrategy>();
        strategyMock.SetupGet(s => s.CountryCode).Returns(countryCode);
        strategyMock.Setup(s => s.VatRegisterAsync(It.IsAny<VatRegistrationRequest>()))
                    .Returns(Task.CompletedTask);

        var service = new VatRegistrationService(new[] { strategyMock.Object });

        var request = new VatRegistrationRequest
        {
            CompanyName = "MyCompany",
            CompanyId = "1",
            Country = countryCode
        };

        // Act
        await service.VatRegisterAsync(request);

        // Assert
        strategyMock.Verify(s => s.VatRegisterAsync(request), Times.Once);
    }
}