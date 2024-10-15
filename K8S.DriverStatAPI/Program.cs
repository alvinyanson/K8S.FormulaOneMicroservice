var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

    var connectionString = builder.Configuration["SecretDefaultConnection"];

    Console.WriteLine($"Relying on K8S configuration: {connectionString}");
    Console.WriteLine($"DriverAchievementAPI: {builder.Configuration["DriverAchievementAPI"]}");
}

else
{
    Console.WriteLine("Relying on local dev machine configuration");
}


var app = builder.Build();

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
