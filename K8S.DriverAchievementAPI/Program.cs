using K8S.DriverAchievementAPI.Data;
using K8S.DriverAchievementAPI.Data.Repositories.Interfaces;
using K8S.DriverAchievementAPI.Profiles;
using K8S.DriverAchievementAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using K8S.DriverAchievementAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// mapster
builder.Services.RegisterMapsterConfiguration();

// connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"Connection string --- {connectionString}");

// initialize db context in DI Container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// mass transit rabbitmq
builder.Services.AddConfigureMassTransitService(builder.Configuration);


var app = builder.Build();

PrepDB.PrepPopulation(app, app.Environment.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
