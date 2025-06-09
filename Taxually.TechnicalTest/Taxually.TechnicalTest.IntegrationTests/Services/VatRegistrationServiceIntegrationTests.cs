using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Taxually.TechnicalTest.Enums;
using Microsoft.Extensions.Configuration;

namespace Taxually.TechnicalTest.IntegrationTests.Services;

public class VatRegistrationServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public VatRegistrationServiceIntegrationTests(WebApplicationFactory<Program> factory)
    {
        var config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false)
      .Build();

        var baseUrl = config["IntegrationTests:BaseUrl"];

        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri(baseUrl)
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