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

var configFolder = builder.Configuration.GetValue<string>("ConfigurationFolder");

const string varName = "DOTNET_RUNNING_IN_CONTAINER";
var runningInContainer = bool.TryParse(Environment.GetEnvironmentVariable(varName),
  out var isRunningInContainer)
  && isRunningInContainer;

if (runningInContainer && !string.IsNullOrWhiteSpace(configFolder) && Directory.Exists(configFolder))
{
    builder.Configuration.AddKeyPerFile(configFolder, true, true);

    Console.WriteLine($"ConfigurationFolder set to: '{configFolder}'.");
    Console.WriteLine($"ConfigurationFolder exists: {Directory.Exists(configFolder)}");
    Console.WriteLine($"Running in Container: '{runningInContainer}'.");

    connectionString = builder.Configuration["SecretDefaultConnection"];

    Console.WriteLine($"Relying on K8S configuration: {connectionString}");
    Console.WriteLine($"DriverStatAPI: {builder.Configuration["DriverStatAPI"]}");
}

else
{
    Console.WriteLine("Relying on local dev machine configuration");
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
