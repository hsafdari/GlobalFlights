using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices.Model
{
    public class TokenResponse
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }=default!;
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = default!;
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; } = default!;
        [JsonPropertyName("state")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TokenState? State { get; set; }
        public DateTime TokenExpiry { get; set; }

    }
    public enum TokenState
    {
        Approved,
        Expired      
    }
}
