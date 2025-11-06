using AutoMapper;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Providers.Amadeus.Models;
using GlobalFlights.ExternalServices.Providers.Lufthansa.Models;
using GlobalFlights.ExternalServices.Providers.TravelPort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices.Providers.Amadeus.Service
{
    public class AmadeusService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public AmadeusService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;          
        }
        public async IAsyncEnumerable<SearchFlightResponseDto> SearchRequestToAPIAsync(SearchFlightRequestDto requestDto)
        {
            //call            
            //var result = await _httpClient.GetAsync("/Providers/Amadeus/FakeData/data.Json");
            //if (!result.IsSuccessStatusCode)
            //{
            //    return null;
            //}
            var path = Path.Combine(AppContext.BaseDirectory, "Providers", "Amadeus", "FakeData", "data.json");
            var result = File.ReadAllText(path);
            var response = JsonSerializer.Deserialize<SearchAmadeusResponse>(result);
            var responseDto = _mapper.Map<SearchFlightResponseDto>(response);
            await Task.Delay(8000);
            yield return responseDto;
        }

       

       
    }
}
