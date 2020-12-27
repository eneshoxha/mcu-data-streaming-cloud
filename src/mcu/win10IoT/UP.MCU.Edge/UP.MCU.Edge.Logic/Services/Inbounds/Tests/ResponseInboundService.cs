using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using Buildersoft.Andy.X.Streams.Model;
using System;
using System.Collections.Generic;
using System.Text;
using UP.MCU.Edge.Logic.Models.Test;

namespace UP.MCU.Edge.Logic.Services.Inbounds.Tests
{
    public class ResponseInboundService
    {
        Source<Models.Test.Response> source;

        public ResponseInboundService(AndyXClient andyXClient)
        {
            var reader = new Reader<Models.Test.Response>(andyXClient.GetClient())
                .Component(Models.Test.Response.ComponentName)
                .Book(Models.Test.Response.BookName)
                .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
                .ReaderName("request-inbound-up-edge-mcu")
                .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
                .Build();

            source = new Source<Models.Test.Response>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }

        private void Source_Integration(object sender, Data<Response> e)
        {
            // Implement the logic of response on MCU.
        }
    }
}
