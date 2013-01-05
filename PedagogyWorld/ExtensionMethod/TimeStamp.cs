using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedagogyWorld.ExtensionMethod
{
    public static class TimeStamp
    {
        public static DateTime ToDateTime(this double input)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(input).ToLocalTime();
        }

        public static double ToUnixTimeStamp(this DateTime input)
        {
            return (input - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
        }
    }
}