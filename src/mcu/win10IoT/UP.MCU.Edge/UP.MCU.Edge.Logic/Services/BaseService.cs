using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.MCU.Edge.Logic.Providers;
using UP.MCU.Edge.Logic.Services.Inbounds.Tests;

namespace UP.MCU.Edge.Logic.Services
{
    public class BaseService
    {
        private readonly AndyXProvider _andyXProvider;

        public BaseService(AndyXProvider andyXProvider)
        {
            _andyXProvider = andyXProvider;
        }

        public void UseAndyXServices(string machineName)
        {
            if (machineName == "bs-andy-mcu-01")
            {
                RequestInboundService requestInboundService = new RequestInboundService(_andyXProvider.GetAndyXClient());
            }
            else
            {
                ResponseInboundService responseInboundService = new ResponseInboundService(_andyXProvider.GetAndyXClient());
            }
        }
    }
}
