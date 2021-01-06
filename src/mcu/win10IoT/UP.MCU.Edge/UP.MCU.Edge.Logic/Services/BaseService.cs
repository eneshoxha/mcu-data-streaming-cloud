using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UP.MCU.Edge.Logic.Providers;
using UP.MCU.Edge.Logic.Services.Inbounds.Tests;
using UP.MCU.Edge.Logic.Services.Inbounds.Time;
using UP.MCU.Edge.Logic.Services.Outbounds.Time;

namespace UP.MCU.Edge.Logic.Services
{
    public class BaseService
    {
        private Timer sendCommandsToMcuTimer;
        private readonly AndyXProvider _andyXProvider;
        private CommandOutboundService commandOutboundService;

        public BaseService(AndyXProvider andyXProvider)
        {
            _andyXProvider = andyXProvider;

            sendCommandsToMcuTimer = new Timer();
            sendCommandsToMcuTimer.Interval = new TimeSpan(0, 0, 1).TotalMilliseconds;
            sendCommandsToMcuTimer.Elapsed += SendCommandsToMcuTimer_Elapsed;
            sendCommandsToMcuTimer.Stop();
        }

        public void UseAndyXServices(string machineName)
        {
            if (machineName == "bs-andy-mcu-01")
            {
                RequestInboundService requestInboundService = new RequestInboundService(_andyXProvider.GetAndyXClient());
                commandOutboundService = new CommandOutboundService(_andyXProvider.GetAndyXClient());
                sendCommandsToMcuTimer.Start();
            }
            else
            {
                ResponseInboundService responseInboundService = new ResponseInboundService(_andyXProvider.GetAndyXClient());
                CommandInboundService commandInboundService = new CommandInboundService(_andyXProvider.GetAndyXClient());
            }
        }

        private async void SendCommandsToMcuTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await commandOutboundService.WriteCommand(new Models.Time.Command()
            {
                Data = DateTime.Now,
                Koha = DateTime.Now.TimeOfDay
            });
        }
    }
}
