using System;
using System.Collections.Generic;
using System.Text;

namespace UP.MCU.Edge.Logic.Models.Test
{
    public class Request
    {
        public static string ComponentName = "test";
        public static string BookName = "test-request";

        public double Autonumber { get; set; }
        public string Message { get; set; }
    }
}