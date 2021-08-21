using System;
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
            var server = GetConnectionString(configuration);
            var client = new MongoClient(server);
            var dbName = configuration[_databaseConfigLocation];
            if (string.IsNullOrWhiteSpace(dbName))
                throw new GenshinException("mongo database is null");
            var database = client.GetDatabase(dbName);
            services.AddScoped<IMongoDatabase>(x => database);
            return services;
        }

        public static IHealthChecksBuilder AddMongoDbHealthCheck(
            this IHealthChecksBuilder builder,
            IConfiguration configuration
        )
        {
            var connectionString = GetConnectionString(configuration);
            builder.AddMongoDb(
                connectionString,
                timeout: TimeSpan.FromSeconds(5)
            );
            return builder;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration[_serverConfigLocation];
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new GenshinException(
                    ConfiguraionEmpty(_serverConfigLocation)
                );
            return connectionString;
        }

        private static string ConfiguraionEmpty(string configLocation) =>
            $"{configLocation} does not have a value";
    }
}