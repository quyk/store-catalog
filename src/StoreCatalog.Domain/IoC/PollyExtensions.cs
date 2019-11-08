using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace StoreCatalog.Domain.IoC
{
    /// <summary>
    /// Usefull methods to help on Polly configurations
    /// </summary>
    public static class PollyExtensions
    {
        /// <summary>
        /// Configure a unique Policy Retry
        /// </summary>
        /// <returns>A IAsyncPolicy<HttpResponseMessage> object with Policy defined</returns>
        public static IAsyncPolicy<HttpResponseMessage> ConfigurePolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttempt =>
                    {
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    });
        }
    }
}
