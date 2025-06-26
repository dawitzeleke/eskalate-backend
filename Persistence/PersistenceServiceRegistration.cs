// Persistence/PersistenceServiceRegistration.cs
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using backend.Persistence.DatabaseContext;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using backend.Application.Contracts.Persistence;
using backend.Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDBSettings"));

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = new MongoClient(settings.ConnectionString);
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}