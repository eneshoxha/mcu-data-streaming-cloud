using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using Buildersoft.Andy.X.Streams.Model;
using UP.MCU.Edge.Logic.Models.Test;
using UP.MCU.Edge.Logic.Services.Outbounds.Tests;

namespace UP.MCU.Edge.Logic.Services.Inbounds.Tests
{
    public class ResponseInboundService
    {
        private readonly Source<Models.Test.Response> source;
        private readonly RequestOutboundService _requestOutboundService;

        public ResponseInboundService(AndyXClient andyXClient)
        {
            var reader = new Reader<Models.Test.Response>(andyXClient.GetClient())
                .Component(Models.Test.Response.ComponentName)
                .Book(Models.Test.Response.BookName)
                .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
                .ReaderName("response-inbound-up-edge-mcu")
                .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
                .Build();

            _requestOutboundService = new RequestOutboundService(andyXClient);

            source = new Source<Response>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }

        private async void Source_Integration(object sender, Data<Response> e)
        {
            await _requestOutboundService.WriteRequest(new Request());
        }
    }
}
