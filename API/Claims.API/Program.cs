using Claims.API.Controllers;
using Claims.BusinessLogic;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Testcontainers.MongoDb;
using Testcontainers.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Start Testcontainers for SQL Server and MongoDB
var sqlContainer = (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
        ? new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        : new()

    ).Build();

var mongoContainer = new MongoDbBuilder()
    .WithImage("mongo:latest")
    .Build();

await sqlContainer.StartAsync();
await mongoContainer.StartAsync();

var sqlConnectionString = sqlContainer.GetConnectionString();
var mongoConnectionString = mongoContainer.GetConnectionString();
var mongoDatabaseName = builder.Configuration["MongoDb:DatabaseName"] ?? "ClaimsDb";

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Delegate ALL service/DB registration to the core layer
builder.Services.AddBusinessLogicAndPersistence(sqlConnectionString, mongoConnectionString, mongoDatabaseName);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// We still need to run migrations. Since API shouldn't depend on Database, we'll
// trigger this via a service in BusinessLogic. BUT, in production i would implement a
// Claims.Database.Migration Project/Service and run it at very first as a init container in k8s.
using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetService<Claims.BusinessLogic.Interfaces.IMigrationService>();
    if (migrationService != null)
    {
        migrationService.ApplyMigrations();
    }
    else
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogWarning("Migration service not found. Migrations not applied.");
    }
}

app.Run();
