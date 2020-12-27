using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace UP.MCU.Edge.Logic.Services.Inbounds.Tests
{
   public class RequestInboundService
    {
        Source<Models.Test.Request> source;
        public RequestInboundService(AndyXClient andyXClient)
        {
            var reader = new Reader<Models.Test.Request>(andyXClient.GetClient())
                .Component(Models.Test.Request.ComponentName)
                .Book(Models.Test.Request.BookName)
                .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
                .ReaderName("request-inbound-up-edge-mcu")
                .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
                .Build();

            source = new Source<Models.Test.Request>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }

        private void Source_Integration(object sender, Buildersoft.Andy.X.Streams.Model.Data<Models.Test.Request> e)
        {
            // Implement the logic of request on MCU.
        }
    }
}
