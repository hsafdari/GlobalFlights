using FluentAssertions;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Model;
using GlobalFlights.ExternalServices.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.GlobalFlights.ExternalServices.Services
{
    public class AuthenticationApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly IAuthentication _authenticationApi;
        public AuthenticationApiTests()
        {
            var apiConfig = new Dictionary<string, string>
             {
                 {"Providers:AmadeusEndpoint:tokenUrl", "https://test.api.amadeus.com/v1/security/oauth2/token" },
                 {"Providers:AmadeusEndpoint:apiKey", "AFH4Mir7tjFhu6Mit0RxEPG4KAkXPljx" },
                 {"Providers:AmadeusEndpoint:apiSecret", "t31J3MqL6dLrczTD" }
             };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(apiConfig).Build();

            _authenticationApi = new AuthenticationApiService(new HttpClient(), configuration);
        }
        [Fact]
        public async Task GetTokenAsync_ReturnSuccess()
        {

            var result = await _authenticationApi.GetTokenAsync();
            result.AccessToken.Should().NotBeNullOrEmpty();
            result.ExpiresIn.Should().BeGreaterThan(0);
            result.TokenType.Should().Be("Bearer");
            result.TokenExpiry.Should().BeAfter(DateTime.UtcNow);
            result.State.Should().Be(TokenState.Approved);
        }
        [Fact]
        public async Task IsTokenValid_ReturnTrue()
        {
            var token = await _authenticationApi.GetTokenAsync();
            var result = _authenticationApi.IsTokenValid(token);
            result.Should().BeTrue();
        }
        [Fact]
        public async Task IsTokenValid_ReturnFalse()
        {
            var token = await _authenticationApi.GetTokenAsync();
            token.TokenExpiry = DateTime.UtcNow.AddMinutes(-45); // Expire the token
            token.ExpiresIn = Convert.ToDateTime(token.TokenExpiry).Second;  // Set ExpiresIn to a negative value
            var result = _authenticationApi.IsTokenValid(token);
            result.Should().BeFalse();
        }
    }
}
