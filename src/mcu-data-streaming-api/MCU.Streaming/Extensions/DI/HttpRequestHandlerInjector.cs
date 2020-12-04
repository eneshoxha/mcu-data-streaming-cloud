using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace MCU.Streaming.Extensions.DI
{
    public static class HttpRequestHandlerInjector
    {
        public static IServiceCollection AddHttpClientHandler(this IServiceCollection services)
        {
            services.AddSingleton<HttpClientHandler>(provider =>
            {
                var httpClientHandler = new HttpClientHandler();
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                }
                return httpClientHandler;
            });

            return services;
        }
    }
}
