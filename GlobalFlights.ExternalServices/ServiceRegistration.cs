using GlobalFlights.ExternalServices.Interfaces;
using GlobalFlights.ExternalServices.Mappings;
using GlobalFlights.ExternalServices.Providers.Amadeus.Service;
using GlobalFlights.ExternalServices.Providers.Lufthansa.Service;
using GlobalFlights.ExternalServices.Providers.TravelPort.Service;
using GlobalFlights.ExternalServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalFlights.ExternalServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterExternalServices(this IServiceCollection services)
        {           
            services.AddScoped<IExternalApiService,AmadeusApiService>();
            services.AddScoped<IExternalApiService, LufthansaApiService>();
            services.AddScoped<IExternalApiService, TravelPortApiService>();
            services.AddScoped<IAuthentication, AuthenticationApiService>();

            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
