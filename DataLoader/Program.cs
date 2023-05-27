// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using DataLoader.Models;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var ServicesConfig = config.GetSection("Services");
var tempServiceConfig = ServicesConfig.GetSection("Temperature");
var tempServiceHost = tempServiceConfig["Host"];
var tempServicePort = tempServiceConfig["Port"];

var precipServiceConfig = ServicesConfig.GetSection("Precipitation");
var precipServiceHost = precipServiceConfig["Host"];
var precipServicePort = precipServiceConfig["Port"];

var zipCodes = new List<string> { "98052", "98011", "98033", "98074", "98021" };

Console.WriteLine("Starting data load...");

var temperatureHttpClient = new HttpClient();
temperatureHttpClient.BaseAddress = new Uri($"http://{tempServiceHost}:{tempServicePort}");

var precipitationHttpClient = new HttpClient();
precipitationHttpClient.BaseAddress = new Uri($"http://{precipServiceHost}:{precipServicePort}");

foreach(var zip in zipCodes)
{
    Console.WriteLine($"Loading data for {zip}...");
    var temps = PostTemperatureData(temperatureHttpClient, zip);
    PostPrecipitationData(temps[0],precipitationHttpClient, zip);
    
}

List<int> PostTemperatureData(HttpClient temperatureHttpClient, string zip)
{
    var rand = new Random();
    var t1 = rand.Next(0, 100);    
    var t2 = rand.Next(0, 100);
    var hiloTemps = new List<int> { t1, t2 };
    hiloTemps.Sort();

    var tempObservation = new TemperatureModel
    {
        Zipcode = zip,
        TempHighF = hiloTemps[1],
        TempLowF = hiloTemps[0],
        CreatedOn = DateTime.UtcNow
    };

    var tempResponse = temperatureHttpClient.PostAsJsonAsync("api/temperature", tempObservation).Result;

    if(tempResponse.IsSuccessStatusCode)
    {
        Console.WriteLine($"Successfully posted temperature data for {zip}");
    }
    else
    {
        Console.WriteLine($"Failed to post temperature data for {zip}");
    }

    return hiloTemps;
}

void PostPrecipitationData(int lowTemp, HttpClient precipitationHttpClient, string zip)
{
    var rand = new Random();
    var isPrecip = rand.Next(2) < 1;

    PrecipitationModel precipitation;

    if(isPrecip)
    {
        var precipInches = rand.Next(1,16);
        if(lowTemp < 32)
        {
            precipitation = new PrecipitationModel
            {
                AmountInches = precipInches,
                ZipCode = zip,
                WeatherType = "Snow",
                CreatedOn = DateTime.UtcNow
            };
        }
        else
        {
            precipitation = new PrecipitationModel
            {
                ZipCode = zip,
                AmountInches = precipInches,
                WeatherType = "Rain",
                CreatedOn = DateTime.UtcNow
            };
        }
    }
    else
    {
        precipitation = new PrecipitationModel
        {
            ZipCode = zip,
            AmountInches = 0,
            WeatherType = "None",
            CreatedOn = DateTime.UtcNow
        };
    }

    var precipResponse = precipitationHttpClient.PostAsJsonAsync("api/precipitation", precipitation).Result;

    if(precipResponse.IsSuccessStatusCode)
    {
        Console.WriteLine($"Successfully posted precipitation data for {zip}");
    }
    else
    {
        Console.WriteLine($"Failed to post precipitation data for {zip}");
    }
}