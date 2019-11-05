using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using StoreCatalog.Domain.IoC;
using Polly.Extensions.Http;
using Polly;
using System;
using System.Net.Http;
using System.Net;
using AutoMapper;
using StoreCatalog.Api.Profiles;

namespace StoreCatalog.Api
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
            services
                .UseServices()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient<IHttpClientFactory>().AddPolicyHandler(
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                    .WaitAndRetryAsync(3, retryAttempt =>
                    {
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    })
            );

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
                c.SwaggerDoc("v1", new Info { Title = "Store Catalog", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Catalog V1");
            });

            app.UseCors("StoreCatalogPolicy");
            app.UseMvc();
        }
    }
}
