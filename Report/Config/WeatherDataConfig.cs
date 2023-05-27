using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Config
{
    public class WeatherDataConfig
    {
        public string PrecipDataProtocol { get; set; }
        public string PrecipDataHost { get; set; }
        public string PrecipDataPort { get; set; }
        public string TempDataProtocol { get; set; }
        public string TempDataHost { get; set; }
        public string TempDataPort { get; set; }
    }
}