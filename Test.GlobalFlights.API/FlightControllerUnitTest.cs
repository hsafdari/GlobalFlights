using GlobalFlights.Common.Enums;
using GlobalFlights.DTOs.Search;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using System.Text.Json;

namespace Test.GlobalFlights.API
{
    public class FlightControllerUnitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        public FlightControllerUnitTest(WebApplicationFactory<Program> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }
        [Fact]
        public async Task SearchFlight_Return()
        {
            //Arrange
            var client = _webApplicationFactory.CreateClient();            
            var requestDto = new SearchFlightRequestDto(Origin: "BCN", DepartureDate: "2025-11-02", OneWay: true,
                                                 Duration: null,
                                                 NonStop: true,
                                                 ViewBy: ResultViewBy.DESTINATION,
                                                 MaxPrice: null,
                                                 IncludedAirlineCodes: null,
                                                 ExcludedAirlineCodes: null,
                                                 TravelClass: null,
                                                 CurrencyCode: "EUR",
                                                 ProviderCode: null);            
            var response=await client.GetAsync("/api/flight/search?"+ requestDto.ToQueryString());
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task CallStreamAction()
        {
            //
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("/api/flight/stream");
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);            
        }
    }
}
