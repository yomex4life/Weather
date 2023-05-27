using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Temperature.DataAccess;

namespace Temperature.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperature _temperature;

        public TemperatureController(ITemperature temperature)
        {
            _temperature = temperature;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataAccess.Temperature>>> GetAll()
        {
            var temperatures = await _temperature.GetAll();
            return Ok(temperatures);
        }

        [HttpGet("{observation}/{zip}")]
        public async Task<ActionResult<string>> Get(string zip)
        {
            var temp = await _temperature.Get(zip);

            return Ok(temp);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DataAccess.Temperature temp)
        {
            var temperature = temp;
            temperature.CreatedOn = temperature.CreatedOn.ToUniversalTime();
            _temperature.Add(temperature);
            return Ok();
        }
    }
}