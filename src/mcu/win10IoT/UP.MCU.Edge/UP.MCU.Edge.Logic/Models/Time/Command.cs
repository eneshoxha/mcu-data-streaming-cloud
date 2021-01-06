using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP.MCU.Edge.Logic.Models.Time
{
    public class Command
    {
        public static string ComponentName = "kontrolleri-kohe";
        public static string BookName = "komanda-kohe";

        public DateTime Data { get; set; }
        public TimeSpan Koha { get; set; }
    }
}
