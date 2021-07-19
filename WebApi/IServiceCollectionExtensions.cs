using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace WebApi
{
    internal static class IServiceCollectionExtensions
    {
        private const string _serverConfigLocation =
            "J5:MongoConnections:Genshin:Server";
        public static IServiceCollection AddMongoDb(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var server = configuration["J5:MongoConnections:Genshin:Server"];
            if (string.IsNullOrWhiteSpace(server))
                throw new GenshinException(
                    ConfiguraionEmpty(_serverConfigLocation)
                );
            var client = new MongoClient(server);
            if (client is null)
                throw new GenshinException("mongo client is null");
            var database = client.GetDatabase(
                configuration["J5:MongoConnections:Genshin:Database"]
            );
            services.AddScoped<IMongoDatabase>(x => database);
            return services;
        }

        private static string ConfiguraionEmpty(string configLocation) =>
            $"{configLocation} does not have a value";
    }
}