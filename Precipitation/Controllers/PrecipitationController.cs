using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Precipitation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrecipitationController : ControllerBase
    {
        public PrecipitationController()
        {
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{observation}/{zip}")]
        public ActionResult<string> Get(string zip)
        {
            return $"Zip: : {zip}";
        }
    }
}