using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Report.DataAccess;

namespace Report.BusinessLogic
{
    public interface IWeatherReportAggregator
    {
        public Task<WeatherReport> BuildWeeklyReport(string zip, int days);
    }
}