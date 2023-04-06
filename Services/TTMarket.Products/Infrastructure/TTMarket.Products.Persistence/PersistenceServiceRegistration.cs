using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TTMarket.Products.Application.Contracts.Persistence;
using TTMarket.Products.Persistence.DatabaseConfig;
using TTMarket.Products.Persistence.Repositories;

namespace TTMarket.Products.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBConnection>(
                configuration.GetSection(nameof(MongoDBConnection)));
            services.AddSingleton<IMongoDBConnection>(provider =>
                provider.GetRequiredService<IOptions<MongoDBConnection>>().Value);
                
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}