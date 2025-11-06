using AutoMapper;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Providers.Lufthansa.Models;
using System.Text.Json;

namespace GlobalFlights.ExternalServices.Providers.Lufthansa.Service
{
    public class LufthansaService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public LufthansaService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public async IAsyncEnumerable<SearchFlightResponseDto> SearchRequestToAPIAsync(SearchFlightRequestDto requestDto)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Providers", "Lufthansa", "FakeData", "data.json");
            var result = File.ReadAllText(path);
            var response = JsonSerializer.Deserialize<SearchLufthansaResponse>(result);
            var responseDto = _mapper.Map<SearchFlightResponseDto>(response);
            await Task.Delay(4000);
            yield return responseDto;
        }
    }
}
