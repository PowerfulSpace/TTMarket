using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TTMarket.Products.Application.Contracts.Mapping;

namespace TTMarket.Products.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            services.AddAutoMapper(cfg => 
            {
                cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });
            return services;
        }
    }
}