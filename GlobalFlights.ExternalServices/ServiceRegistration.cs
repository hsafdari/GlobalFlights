using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Mappings;
using GlobalFlights.ExternalServices.Providers.Amadeus.Service;
using GlobalFlights.ExternalServices.Providers.Lufthansa.Service;
using GlobalFlights.ExternalServices.Providers.TravelPort.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterExternalServices(this IServiceCollection services)
        {           
            services.AddScoped<IApiService,AmadeusService>();
            services.AddScoped<IApiService, LufthansaService>();
            services.AddScoped<IApiService, TravelPortService>();
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
