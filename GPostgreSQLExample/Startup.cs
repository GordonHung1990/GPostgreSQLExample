using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GPostgreSQLExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                string[] xmlFileNames = null;
                xmlFileNames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");
                foreach (var xmlFileName in xmlFileNames)
                {
                    c.IncludeXmlComments(xmlFileName);
                }
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GPostgreSQLExample", Version = "v1" });
            });
            services
                .AddRepositories(
                   (sp, dbBuilder) =>
                   {
                       var config = sp.GetRequiredService<IConfiguration>();

                       dbBuilder
                           .UseLazyLoadingProxies();

                       dbBuilder.UseNpgsql(
                           config.GetConnectionString("Default"),
                           serverOption =>
                           {
                               serverOption.SetPostgresVersion(12, 5);
                               serverOption.CommandTimeout(180);
                               serverOption.UseTrigrams();
                           });

                       dbBuilder.UseSnakeCaseNamingConvention();
                   })
                .AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GPostgreSQLExample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}