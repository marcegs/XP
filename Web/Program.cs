using System.Text.Json.Serialization;
using Application;
using Infra;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var config = builder.Configuration;

    builder.Services.AddSerilog();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddProblemDetails();

    builder.Services.AddApplication();
    builder.Services.AddPersistence(config);
    builder.Services.AddInfra(config);

    var app = builder.Build();
    
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler();

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application closed with errors");
}
finally
{
    await Log.CloseAndFlushAsync();
}