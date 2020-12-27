using Buildersoft.Andy.X.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UP.MCU.Edge.Logic.Services.Outbounds.Tests
{
    public class ResponseOutboundService
    {
        private Writer<Models.Test.Response> writer;
        public ResponseOutboundService(AndyXClient andyXClient)
        {
            writer = new Writer<Models.Test.Response>(andyXClient.GetClient())
                .Component(Models.Test.Response.ComponentName)
                .Book(Models.Test.Response.BookName)
                .Schema(Buildersoft.Andy.X.Client.Model.SchemaTypes.Json)
                .WriterType(Buildersoft.Andy.X.Client.Model.WriterTypes.StreamAndStore)
                .Build();
        }

        public async Task<Guid> WriteResponse(Models.Test.Response response)
        {
            return await writer.WriteAsync(response);
        }
    }
}
