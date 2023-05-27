using Microsoft.EntityFrameworkCore;
using Report.BusinessLogic;
using Report.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReportDbContext>(options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Report"));
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);
builder.Services.AddScoped<IWeatherReportAggregator, WeatherReportAggregator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

