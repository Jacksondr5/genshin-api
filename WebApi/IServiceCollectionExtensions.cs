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
        private const string _databaseConfigLocation =
            "J5:MongoConnections:Genshin:Database";
        public static IServiceCollection AddMongoDb(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var server = configuration[_serverConfigLocation];
            if (string.IsNullOrWhiteSpace(server))
                throw new GenshinException(
                    ConfiguraionEmpty(_serverConfigLocation)
                );
            var client = new MongoClient(server);
            if (client is null)
                throw new GenshinException("mongo client is null");
            var dbName = configuration[_databaseConfigLocation];
            if (string.IsNullOrWhiteSpace(dbName))
                throw new GenshinException("mongo database is null");
            var database = client.GetDatabase(dbName);
            services.AddScoped<IMongoDatabase>(x => database);
            return services;
        }

        private static string ConfiguraionEmpty(string configLocation) =>
            $"{configLocation} does not have a value";
    }
}