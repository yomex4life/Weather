using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Precipitation.DataAccess;

namespace Precipitation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrecipitationController : ControllerBase
    {
        private readonly IPrecipitate _precipitate;

        public PrecipitationController(IPrecipitate precipitate)
        {
            _precipitate = precipitate;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataAccess.Precipitation>>> GetAll()
        {
            var precipitations = await _precipitate.GetAll();
            return Ok(precipitations);
        }

        [HttpGet("{zip}")]
        public async Task<ActionResult<IEnumerable<DataAccess.Precipitation>>> GetByZip(string zip)
        {
            var precip = await _precipitate.Get(zip);

            return Ok(precip);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DataAccess.Precipitation precip)
        {
            var precipitation = precip;
            precipitation.CreatedOn = precipitation.CreatedOn.ToUniversalTime();
            _precipitate.Add(precipitation);
            return Ok();
        }
    }
}