using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.MCU.RealtimeReporting.Models.Time
{
    public class Event
    {
        public static string ComponentName = "kontrolleri-kohe";
        public static string BookName = "ngjarja-kohe";

        public DateTime Data { get; set; }
        public string Koha { get; set; }

        public string KohaEPranuar { get; set; }
        public double DiferencaNeMilisekonda { get; set; }
    }
}
