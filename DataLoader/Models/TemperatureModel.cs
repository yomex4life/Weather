using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLoader.Models
{
    public class TemperatureModel
    {
        public DateTime CreatedOn { get; set; }
        public decimal TempHighF { get; set; }
        public decimal TempLowF { get; set; }
        public string Zipcode { get; set; }
    }
}