using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Model;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GlobalFlights.ExternalServices.Services
{
    public class AuthenticationApiService : IAuthentication
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;        
        public AuthenticationApiService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public string AccessToken { get; set; } = null!;
       
        public async Task<TokenResponse> GetTokenAsync()
        {
            string baseAddress = _configuration.GetSection("Providers:AmadeusEndpoint:tokenUrl").Value;
           
            string client_id = _configuration.GetSection("Providers:AmadeusEndpoint:apiKey").Value!;
            string client_secret = _configuration.GetSection("Providers:AmadeusEndpoint:apiSecret").Value!;
            var requestBody = new StringContent(
            $"grant_type=client_credentials&client_id={client_id}&client_secret={client_secret}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        );
            var responseMessage = await _httpClient.PostAsync(baseAddress, requestBody);
            var token =await responseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(token))
                throw new Exception("Token could not be retrieved from authentication provider.");
            var responseJson= JsonSerializer.Deserialize<TokenResponse>(token);
            if (responseJson == null)
                throw new Exception("Token response could not be deserialized.");
            if (responseJson.State!=TokenState.Approved)
                throw new Exception("Token not approved try");
            responseJson.TokenExpiry=DateTime.UtcNow.AddSeconds(responseJson.ExpiresIn);
            AccessToken = responseJson.AccessToken;
            return responseJson;
        }
        public bool IsTokenValid(TokenResponse token)
        {            
            return DateTime.UtcNow >= token.TokenExpiry? false:true;
        }
    }
}
