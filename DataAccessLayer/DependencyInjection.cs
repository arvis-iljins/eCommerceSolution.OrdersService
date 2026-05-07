using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.OrderMicroservice.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionStringTemplate = configuration.GetConnectionString("MongoDB");
        var connectionString = connectionStringTemplate!
            .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGO_HOST")!)
            .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGO_PORT")!);

        services.AddSingleton<IMongoClient, MongoClient>(provider => new MongoClient(
            connectionString
        ));
        services.AddScoped<IMongoDatabase>(provider =>
        {
            var mongoClient = provider.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase("Orders");
        });

        return services;
    }
}
