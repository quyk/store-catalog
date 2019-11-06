using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using StoreCatalog.Api.Profiles;
using StoreCatalog.Domain.IoC;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace StoreCatalog.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .UseOptions(Configuration)                
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureHttpClients(Configuration);
            
            services.AddAutoMapper(typeof(AreasModelProfile),
                                   typeof(ProductModelProfile));

            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("StoreCatalogPolicy",
                builder =>
                {
                    builder.WithMethods("GET", "OPTIONS", "HEAD");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info 
                { 
                    Title = "Store Catalog", 
                    Version = "v1",
                    Description = "StoreCatalog Microservice from GeekBurger"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.UseServices();
            services.UseStart().GetAwaiter().GetResult();
            services.UseServiceBus().GetAwaiter().GetResult();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Catalog V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("StoreCatalogPolicy");
            app.UseMvc();
        }
    }
}
