using Microsoft.EntityFrameworkCore;
using Precipitation.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPrecipitate, Precipitate>();
builder.Services.AddDbContext<PrecipDbContext>(options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Precipitation"));
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

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

