using K8S.DriverAPI.Data;
using K8S.DriverAPI.Data.Repositories;
using K8S.DriverAPI.Data.Repositories.Interfaces;
using K8S.DriverAPI.Profiles;
using K8S.Microservice.Driver.Data;
using Microsoft.EntityFrameworkCore;

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


const string varName = "DOTNET_RUNNING_IN_CONTAINER";
var runningInContainer = bool.TryParse(Environment.GetEnvironmentVariable(varName),
  out var isRunningInContainer)
  && isRunningInContainer;

var configFolder = builder.Configuration.GetValue<string>("ConfigurationFolder");

if (runningInContainer && !string.IsNullOrWhiteSpace(configFolder) && Directory.Exists(configFolder))
{
    builder.Configuration.AddKeyPerFile(configFolder, false, true);

    Console.WriteLine($"1ConfigurationFolder set to: '{configFolder}'.");
    Console.WriteLine($"1ConfigurationFolder exists: {Directory.Exists(configFolder)}");
    Console.WriteLine($"1Running in Container: '{runningInContainer}'.");

    Console.WriteLine("1Relying on default configuration");
}

else
{
    Console.WriteLine($"2ConfigurationFolder set to: '{configFolder}'.");
    Console.WriteLine($"2ConfigurationFolder exists: {Directory.Exists(configFolder)}");
    Console.WriteLine($"2Running in Container: '{runningInContainer}'.");

    Console.WriteLine("2Relying on default configuration");
}

// initialize db context in DI Container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
