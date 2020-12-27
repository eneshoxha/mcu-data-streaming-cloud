using Buildersoft.Andy.X.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UP.MCU.Edge.Logic.Services.Outbounds.Tests
{
    public class RequestOutboundService
    {
        private Writer<Models.Test.Request> writer;
        public RequestOutboundService(AndyXClient andyXClient)
        {
            writer = new Writer<Models.Test.Request>(andyXClient.GetClient())
                .Component(Models.Test.Request.ComponentName)
                .Book(Models.Test.Request.BookName)
                .Schema(Buildersoft.Andy.X.Client.Model.SchemaTypes.Json)
                .WriterType(Buildersoft.Andy.X.Client.Model.WriterTypes.StreamAndStore)
                .Build();
        }

        public async Task<Guid> WriteRequest(Models.Test.Request request)
        {
            return await writer.WriteAsync(request);
        }
    }
}
