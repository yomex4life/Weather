using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Report.BusinessLogic;

namespace Report.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IWeatherReportAggregator _aggregator;

        public ReportController(IWeatherReportAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string zip)
        {
            var report = await _aggregator.BuildWeeklyReport(zip);
            return Ok(report);
        }
    }
}