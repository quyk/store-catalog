using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.HttpClientFactory;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using StoreCatalog.Domain.Models.Product;
using StoreCatalog.Domain.Models.Store;
using StoreCatalog.Domain.ServiceBus;
using StoreCatalog.Domain.ServiceBus.Receiver;
using StoreCatalog.Domain.ServiceBus.Topic;
using System;

namespace StoreCatalog.Domain.IoC
{
    /// <summary>
    /// Usefull IServiceCollection extension methods
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Add services Dependency Injection from project
        /// </summary>
        /// <param name="services">A Microsoft.Extensions.DependencyInjection.IServiceCollection</param>
        /// <returns>Microsoft.Extensions.DependencyInjection.IServiceCollection with services</returns>
        public static IServiceCollection UseServices(this IServiceCollection services)
        {
            services.AddTransient<IStoreCatalogClientFactory, StoreCatalogClientFactory>();
            services.AddSingleton<IAreaService, AreaService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IStoreService, StoreService>();

            services.AddTransient<IReceiverBus, ReceiverBus>();
            services.AddTransient<IQueueBus, QueueBus>();
            services.AddTransient<ITopicBus, TopicBus>();
            return services;
        }

        /// <summary>
        /// Method to configure all HttpClients with Polly Policy
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection with HttpClients</returns>
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("Products", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("ProductBaseUrl"));
            }).AddPolicyHandler(PollyExtensions.ConfigurePolicy());

            services.AddHttpClient("Areas", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("AreaBaseUrl"));
            }).AddPolicyHandler(PollyExtensions.ConfigurePolicy());

            services.AddHttpClient("Ingredients", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("IngredientsUrl"));
            }).AddPolicyHandler(PollyExtensions.ConfigurePolicy());

            return services;
        }
    }
}
