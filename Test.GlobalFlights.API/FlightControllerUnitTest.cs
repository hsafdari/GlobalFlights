using GlobalFlights.Common.Enums;
using GlobalFlights.DTOs.Search;
using Microsoft.AspNetCore.Mvc.Testing;

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
