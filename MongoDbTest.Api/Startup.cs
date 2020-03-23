using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDbTest.Api.Infrastructure;

namespace MongoDbTest.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuracion</param>
        /// <param name="env">Ambiente</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("settings/appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Configuracion
        /// </summary>
        public IConfiguration Configuration { get; }

        private readonly string AllowSpecificOrigins = nameof(AllowSpecificOrigins);

        /// <summary>
        /// Agregar metodos y servicios
        /// </summary>
        /// <param name="services">Servicios</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Initialize(Configuration);
            services.AddMvc();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
        }

        /// <summary>
        /// Configurar los HTTP request pipeline.
        /// </summary>
        /// <param name="app">Aplicacion</param>
        /// <param name="env">Ambiente</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(Configuration);
            app.UseCors(AllowSpecificOrigins);
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
