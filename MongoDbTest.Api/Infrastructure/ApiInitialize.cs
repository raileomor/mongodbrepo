using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDbTest.Api.Infrastructure.Extensions;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Services;
using MongoDbTest.Infrastructure.Repositories;
using MongoDbTest.Infrastructure.Repositories.Configurations;
using MongoDbTest.Infrastructure.RestClients;

namespace MongoDbTest.Api.Infrastructure
{
    /// <summary>
    /// Initialize
    /// </summary>
    public static class ApiInitialize
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration(configuration);
            services.AddSwaggerDocumentation(configuration);
            services.AddScoped(configuration);
            services.AddClients(configuration);
        }

        /// <summary>/
        /// Add services
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        public static void AddScoped(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoDbRepositoryContext, MongoDbRepositoryContext>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IMongoDbAccountRepository, MongoDbAccountRepository>();
        }

        /// <summary>/
        /// Add clients
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        public static void AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IDocumentApiClient, DocumentApiClient>();
        }

        /// <summary>
        /// Define API features
        /// </summary>
        /// <param name="app">Application</param>
        /// <param name="configuration">Configuration</param>
        public static void Use(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwaggerDocumentation();
        }

        /// <summary>
        /// Add configuration
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(options => configuration.GetSection(MongoDbConfiguration.Key).Bind(options));
        }
    }
}
