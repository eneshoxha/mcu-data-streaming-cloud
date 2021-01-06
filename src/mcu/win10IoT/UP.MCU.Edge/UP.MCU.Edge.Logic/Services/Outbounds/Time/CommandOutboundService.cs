using Buildersoft.Andy.X.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.MCU.Edge.Logic.Models.Time;

namespace UP.MCU.Edge.Logic.Services.Outbounds.Time
{
    public class CommandOutboundService
    {
        private readonly Writer<Command> writer;
        public CommandOutboundService(AndyXClient andyXClient)
        {
            writer = new Writer<Command>(andyXClient.GetClient())
                .Component(Command.ComponentName)
                .Book(Command.BookName)
                .Schema(Buildersoft.Andy.X.Client.Model.SchemaTypes.Json)
                .WriterType(Buildersoft.Andy.X.Client.Model.WriterTypes.StreamAndStore)
                .Build();
        }

        public async Task<Guid> WriteCommand(Command command)
        {
            return await writer.WriteAsync(command);
        }
    }
}
