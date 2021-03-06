﻿using Buildersoft.Andy.X.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace UP.MCU.Edge.Logic.Providers
{
    public class AndyXProvider
    {
        private readonly AndyXClient _andyXClient;
        private bool isBuild = true;
        public AndyXProvider(string url, string tenant, string product, string token)
        {
            _andyXClient = new AndyXClient(url)
                .Tenant(tenant)
                .Product(product)
                .Token(token);

            _andyXClient.BuildAsync();
        }

        public AndyXClient GetAndyXClient()
        {
            return _andyXClient;
        }

        public bool IsAndyXClientConnected()
        {
            return isBuild;
        }
    }
}
