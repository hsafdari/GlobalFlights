using AutoMapper;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Mappings;
using GlobalFlights.ExternalServices.Providers.Amadeus.Service;
using GlobalFlights.ExternalServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Test.GlobalFlights.API.Providers
{
    public class AmadeusApiServiceTest
    {
        private readonly IAuthentication _authentication;
        private readonly IExternalApiService _externalApiService;
        private readonly IMapper _mapper;
        //private readonly ILogger<AmadeusApiService> _logger;
        
        public AmadeusApiServiceTest()
        {
            var config= new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var client = new HttpClient();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            },null);
            _mapper = mapperConfig.CreateMapper();

            
            _authentication = new AuthenticationApiService(client, config);
            _externalApiService = new AmadeusApiService(client, _mapper, _authentication);
        }
        //[Fact]
        //public Task FetchDataAsync_ReturnsFlgihtsDto()
        //{
        //    //Arrange
            
        //    //Act
        //    //Assert
        //    throw new NotImplementedException();
        //}
    }
}
