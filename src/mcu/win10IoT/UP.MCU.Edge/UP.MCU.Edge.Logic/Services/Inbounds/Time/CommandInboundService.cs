using Buildersoft.Andy.X.Client;
using Buildersoft.Andy.X.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.MCU.Edge.Logic.Models.Time;
using UP.MCU.Edge.Logic.Services.Outbounds.Time;

namespace UP.MCU.Edge.Logic.Services.Inbounds.Time
{
    public class CommandInboundService
    {
        private readonly Source<Command> source;
        private readonly EventOutboundService _eventOutboundService;
        public CommandInboundService(AndyXClient andyXClient)
        {
            var reader = new Reader<Command>(andyXClient.GetClient())
                .Component(Command.ComponentName)
                .Book(Command.BookName)
                .ReaderType(Buildersoft.Andy.X.Client.Model.ReaderTypes.Exclusive)
                .ReaderName("command-time-mcu-02")
                .ReaderAs(Buildersoft.Andy.X.Client.Model.ReaderAs.Subscription)
                .Build();

            _eventOutboundService = new EventOutboundService(andyXClient);

            source = new Source<Command>(reader)
                .Configure(new Buildersoft.Andy.X.Streams.Settings.SourceConfigurationSettings())
                .Throttle(1, 1);

            source.Integration += Source_Integration;
            source.InitializeReader();
        }

        private async void Source_Integration(object sender, Buildersoft.Andy.X.Streams.Model.Data<Command> e)
        {
            TimeSpan kohaEPranuar = DateTime.Now.TimeOfDay;
            Event @event = new Event()
            {
                Data = e.RawData.Data,
                Koha = e.RawData.Koha,
                KohaEPranuar = kohaEPranuar.ToString(),
                DiferencaNeMilisekonda = (kohaEPranuar - TimeSpan.Parse(e.RawData.Koha)).TotalMilliseconds
            };
            await _eventOutboundService.WriteEvent(@event);
        }
    }
}
