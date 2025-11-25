using AutoMapper;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Providers.TravelPort.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices.Providers.TravelPort.Service
{
    public class TravelPortApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        private readonly IMapper _mapper;
        public TravelPortApiService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async IAsyncEnumerable<SearchFlightResponseDto> FetchDataAsync(SearchFlightRequestDto requestDto)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Providers", "TravelPort", "FakeData", "data.json");
            var result = File.ReadAllText(path);
            var response = JsonSerializer.Deserialize<SearchTravelPortResponse>(result);
            var responseDto = _mapper.Map<SearchFlightResponseDto>(response);
            await Task.Delay(2000);
            yield return responseDto;
        }
    }
}
