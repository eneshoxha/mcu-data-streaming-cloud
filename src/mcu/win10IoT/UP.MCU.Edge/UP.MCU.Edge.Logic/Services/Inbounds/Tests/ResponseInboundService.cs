using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using Buildersoft.Andy.X.Streams.Model;
using System;
using System.Threading;
using UP.MCU.Edge.Logic.Models.Test;
using UP.MCU.Edge.Logic.Services.Outbounds.Tests;

namespace UP.MCU.Edge.Logic.Services.Inbounds.Tests
{
    public class ResponseInboundService
    {
        private readonly Source<Response> source;
        private readonly RequestOutboundService _requestOutboundService;

        public ResponseInboundService(AndyXClient andyXClient)
        {
            var reader = new Reader<Response>(andyXClient.GetClient())
                .Component(Response.ComponentName)
                .Book(Response.BookName)
                .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
                .ReaderName("response-inbound-up-edge-mcu")
                .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
                .Build();

            _requestOutboundService = new RequestOutboundService(andyXClient);

            _requestOutboundService.WriteRequest(new Request() { Message = "IncrementNumber", Autonumber = 1 });

            source = new Source<Response>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }

        private async void Source_Integration(object sender, Data<Response> e)
        {
            Thread.Sleep(new TimeSpan(0, 0, 10));
            await _requestOutboundService.WriteRequest(new Request() { Message = e.RawData.Message, Autonumber = ++e.RawData.Autonumber });
        }
    }
}
