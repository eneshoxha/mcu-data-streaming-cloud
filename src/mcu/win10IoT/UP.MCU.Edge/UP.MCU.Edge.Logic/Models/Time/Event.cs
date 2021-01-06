using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.MCU.Edge.Logic.Models.Time
{
    public class Event
    {
        public static string ComponentName = "kontrolleri-kohe";
        public static string BookName = "ngjarja-kohe";

        public DateTime Data { get; set; }
        public TimeSpan Koha { get; set; }

        public TimeSpan KohaEPranuar { get; set; }
        public double DiferencaNeMilisekonda { get; set; }
    }
}
