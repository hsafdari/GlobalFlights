using AutoMapper;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Providers.Amadeus.Models;
using System.Text.Json;

namespace GlobalFlights.ExternalServices.Providers.Amadeus.Service
{
    public class AmadeusApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IAuthentication _authentication;
        public AmadeusApiService(HttpClient httpClient, IMapper mapper, IAuthentication authentication)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _authentication = authentication;
        }
        public async IAsyncEnumerable<SearchFlightResponseDto> FetchDataAsync(SearchFlightRequestDto requestDto)
        {
            //call For Test with Fake Data        
            var token =await _authentication.GetTokenAsync();
            if (token is null)
            {
                throw new Exception("Amadeus API token is null");
            }
            _httpClient.BaseAddress = new Uri("https://api.amadeus.com/v2/shopping/flight-offers?");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
            var responseMessage = await _httpClient.GetAsync($"v2/shopping/flight-offers?{requestDto.ToQueryString()}");
            var messages = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<SearchAmadeusResponse>(messages);
            var responseDto = _mapper.Map<SearchFlightResponseDto>(response);
           // await Task.Delay(8000);
            yield return responseDto;
        }
    }
}
