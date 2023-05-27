using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLoader.Models
{
    public class PrecipitationModel
    {
        public DateTime CreatedOn { get; set; }
        public decimal AmountInches { get; set; }
        public string WeatherType { get; set; }
        public string ZipCode { get; set; }
    }
}