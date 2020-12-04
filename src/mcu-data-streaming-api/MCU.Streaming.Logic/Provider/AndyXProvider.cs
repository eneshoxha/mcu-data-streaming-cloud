using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Client.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MCU.Streaming.Logic.Provider
{
    public class AndyXProvider
    {
        private readonly AndyXClient _andyXClient;
        private bool isClientBuilt;

        public AndyXProvider(IConfiguration configuration, HttpClientHandler httpHandler, ILoggerFactory loggerFactory)
        {
            isClientBuilt = false;
            _andyXClient = new AndyXClient(configuration["AndyX:Url"], loggerFactory)
                .HttpClientHandler(httpHandler)
                .Tenant(configuration["AndyX:Tenant"])
                .Product(configuration["AndyX:Product"])
                .Token(configuration["AndyX:Token"]);

            isClientBuilt = _andyXClient.BuildAsync().Result;
        }

        public AndyXClient GetAndyXClient()
        {
            return _andyXClient;
        }

        public AndyXProperty GetAndyXProperty()
        {
            return _andyXClient.GetClient();
        }

        public bool IsClientBuilt()
        {
            return isClientBuilt;
        }
    }
}
