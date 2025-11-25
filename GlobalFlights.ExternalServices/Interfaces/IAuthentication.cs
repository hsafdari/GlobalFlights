using GlobalFlights.ExternalServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices.Interfaces
{
    public interface IAuthentication
    {
        public string AccessToken { get; set; }
        Task<TokenResponse> GetTokenAsync();        
        bool IsTokenValid(TokenResponse token);
    }
}
