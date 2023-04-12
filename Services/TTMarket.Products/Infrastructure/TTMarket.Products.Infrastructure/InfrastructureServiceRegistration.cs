using Microsoft.Extensions.DependencyInjection;
using TTMarket.Products.Application.Contracts.Logger;
using TTMarket.Products.Infrastructure.Logging;

namespace TTMarket.Products.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
            return services;
        }
    }
}