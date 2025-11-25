using Microsoft.Extensions.DependencyInjection;
using Mediator;
using GlobalFlights.Application.Behaviors;

namespace GlobalFlights.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services) {

            services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));            
            return services;
        }
    }
}