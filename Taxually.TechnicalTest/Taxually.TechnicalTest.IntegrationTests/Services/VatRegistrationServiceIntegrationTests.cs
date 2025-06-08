using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Taxually.TechnicalTest.Enums;

namespace Taxually.TechnicalTest.IntegrationTests.Services;

public class VatRegistrationServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public VatRegistrationServiceIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:7132")
        });
    }

    [Theory]
    [InlineData(CountryCode.GB)]
    [InlineData(CountryCode.FR)]
    [InlineData(CountryCode.DE)]
    public async Task VatRegistrationService_W_ReturnsOk(CountryCode countryCode)
    {
        var request = new
        {
            companyName = "MyCompany",
            companyId = "1",
            country = countryCode
        };

        var response = await _client.PostAsJsonAsync("/api/vatregistration", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}