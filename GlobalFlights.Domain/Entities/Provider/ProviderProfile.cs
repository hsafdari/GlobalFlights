using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.Domain.Entity.Provider
{
    public class ProviderProfile
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string RequestBaseUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
