using Microsoft.EntityFrameworkCore;
using Temperature.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TempDbContext>(options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Temperature"));
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);
builder.Services.AddScoped<ITemperature, TempRepo>();

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

