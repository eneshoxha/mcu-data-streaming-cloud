using Buildersoft.Andy.X.Client;
using System;
using System.Threading.Tasks;
using UP.MCU.Edge.Logic.Models.Time;

namespace UP.MCU.Edge.Logic.Services.Outbounds.Time
{
    public class EventOutboundService
    {
        private readonly Writer<Event> writer;
        public EventOutboundService(AndyXClient andyXClient)
        {
            writer = new Writer<Event>(andyXClient.GetClient())
                .Component(Event.ComponentName)
                .Book(Event.BookName)
                .Schema(Buildersoft.Andy.X.Client.Model.SchemaTypes.Json)
                .WriterType(Buildersoft.Andy.X.Client.Model.WriterTypes.StreamAndStore)
                .Build();
        }

        public async Task<Guid> WriteEvent(Event @event)
        {
            return await writer.WriteAsync(@event);
        }
    }
}
