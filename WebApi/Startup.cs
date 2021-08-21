using Core;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{

    public class Startup
    {
        private const string CorsPolicyName = "_k8s.j5 policy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
                o.AddPolicy(
                    name: CorsPolicyName,
                    builder =>
                    {
                        //builder
                        //    .WithOrigins("http://*.j5")
                        //    .SetIsOriginAllowedToAllowWildcardSubdomains();
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                )
            );
            services.AddScoped<IArtifactService, ArtifactService>();
            services.AddScoped<IArtifactRepository, ArtifactRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddMongoDb(Configuration);
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "WebApi", Version = "v1" }
                );
            });
            services.AddHealthChecks().AddMongoDbHealthCheck(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "WebApi v1"
                ));
            }

            //For when you want to do HTTPS
            //https://github.com/dotnet/dotnet-docker/blob/main/samples/host-aspnetcore-https.md
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
