using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Temperature.DataAccess
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal TempHighF { get; set; }
        public decimal TempLowF { get; set; }
        public string Zipcode { get; set; }
    }
}