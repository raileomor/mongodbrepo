using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDbTest.Api.Infrastructure.Configurations;

namespace MongoDbTest.Api.Infrastructure.Extensions
{
    internal static class SwaggerExtension
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration Configuration)
        {
            var swaggerSection = Configuration.GetSection(nameof(SwaggerConfiguration));
            services.Configure<SwaggerConfiguration>(swaggerSection);

            IServiceProvider provider = services.BuildServiceProvider();

            SwaggerConfiguration swaggerConfig = GetSwaggerConfiguration(provider);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerConfig.Version, swaggerConfig.GetOpenApiInfo());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            app.UseDefaultFiles();
            SwaggerConfiguration swaggerConfig = GetSwaggerConfiguration(provider);
            app.UsePathBase($"/{swaggerConfig.HostPath}/");
            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var schema = httpReq.Host.Value.Contains("localhost") ? "http" : "https";
                    swagger.Servers = new List<OpenApiServer> {
                        new OpenApiServer {
                            Description ="Uri Base Api",
                            Url = $"{schema}://{httpReq.Host.Value}"+((!string.IsNullOrEmpty(swaggerConfig.HostPath)) ? $"/{swaggerConfig.HostPath}" : string.Empty)
                        }
                    };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{swaggerConfig.EndpointUrl}", swaggerConfig.EndpointDescription);
            });

            return app;
        }

        private static SwaggerConfiguration GetSwaggerConfiguration(IServiceProvider provider)
        {
            var swaggerConfigurationOption = provider.GetRequiredService<IOptions<SwaggerConfiguration>>();
            SwaggerConfiguration result = swaggerConfigurationOption.Value;
            return result;
        }
    }
}
