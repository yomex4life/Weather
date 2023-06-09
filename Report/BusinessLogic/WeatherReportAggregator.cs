using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Report.Config;
using Report.DataAccess;
using Report.Models;

namespace Report.BusinessLogic
{
    public class WeatherReportAggregator : IWeatherReportAggregator
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<WeatherReportAggregator> _logger;
        private readonly WeatherDataConfig _config;
        private readonly ReportDbContext _context;

        public WeatherReportAggregator(IHttpClientFactory factory, ILogger<WeatherReportAggregator> logger, IOptions<WeatherDataConfig> config, ReportDbContext context)
        {
            _factory = factory;
            _logger = logger;
            _config = config.Value;
            _context = context;
        }
        public async Task<WeatherReport> BuildWeeklyReport(string zip)
        {
            var httpClient = _factory.CreateClient();
            var precipData = await FetchPrecipitationData(httpClient, zip);
            var totalSnow = GetTotalSnow(precipData);
            var totalRain = GetTotalRain(precipData);
            _logger.LogInformation($"Total snow: {totalSnow} inches");
            _logger.LogInformation($"Total rain: {totalRain} inches");
            
            var tempData = await FetchTemperatureData(httpClient, zip);
            var averageHighTemp = tempData.Average(t => t.TempHighF);
            var averageLowTemp = tempData.Average(t => t.TempLowF);

            _logger.LogInformation($"Average high temp: {averageHighTemp} degrees F");
            _logger.LogInformation($"Average low temp: {averageLowTemp} degrees F");

            var totalWeatherReport = new WeatherReport
            {
                AverageHighF = Math.Round(averageHighTemp, 2),
                AverageLowF = Math.Round(averageLowTemp, 2),
                RainfallTotalInches = totalRain,
                SnowTotalInches = totalSnow,
                ZipCode = zip,
                CreatedOn = DateTime.UtcNow
            };

            _context.WeatherReports.Add(totalWeatherReport);
            await _context.SaveChangesAsync();

            return totalWeatherReport;
        }

        private static decimal GetTotalRain(List<PrecipitationModel> precipData)
        {
            var totalRain = precipData.Where(p => p.WeatherType == "Rain").Sum(p => p.AmountInches);
            return Math.Round(totalRain, 2);
        }

        private static decimal GetTotalSnow(List<PrecipitationModel> precipData)
        {
            var totalSnow = precipData.Where(p => p.WeatherType == "Snow").Sum(p => p.AmountInches);
            return Math.Round(totalSnow, 2);
        }

        private async Task<List<TemperatureModel>> FetchTemperatureData(HttpClient httpClient, string zip)
        {
            var endpoint = BuildTemperatureEndpoint(zip);
            Console.WriteLine($"Fetching temperature data from {endpoint}");
            var temperatureRecords = await httpClient.GetAsync(endpoint);

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var temperatureData = await temperatureRecords.Content.ReadFromJsonAsync<List<TemperatureModel>>(jsonSerializerOptions);

            return temperatureData ?? new List<TemperatureModel>();
        }

        private string? BuildTemperatureEndpoint(string zip)
        {
            var tempServiceProtocol = _config.TempDataProtocol;
            var tempServiceHost = _config.TempDataHost;
            var tempServicePort = _config.TempDataPort;
            //return $"{tempServiceProtocol}://{tempServiceHost}:{tempServicePort}/api/temperature?zip={zip}&days={days}"
            return $"{tempServiceProtocol}://{tempServiceHost}:{tempServicePort}/api/temperature/{zip}";
        }

        private async Task<List<PrecipitationModel>> FetchPrecipitationData(HttpClient httpClient, string zip)
        {
            var endpoint = BuildPrecipitationEndpoint(zip);
            var precipitationRecords = await httpClient.GetAsync(endpoint);
            //var precipitationData = await precipitationRecords.Content.ReadFromJsonAsync<List<PrecipitationModel>>();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var precipData = await precipitationRecords.Content.ReadFromJsonAsync<List<PrecipitationModel>>(jsonSerializerOptions);

            return precipData ?? new List<PrecipitationModel>();
        }

        private string? BuildPrecipitationEndpoint(string zip)
        {
            var precipServiceProtocol = _config.PrecipDataProtocol;
            Console.WriteLine($"precipServiceProtocol: {precipServiceProtocol}");
            var precipServiceHost = _config.PrecipDataHost;
            Console.WriteLine($"precipServiceHost: {precipServiceHost}");
            var precipServicePort = _config.PrecipDataPort;
            Console.WriteLine($"precipServicePort: {precipServicePort}");
            //return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/api/precipitation?zip={zip}&days={days}"
            return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/api/precipitation/{zip}";
        }
    }
}