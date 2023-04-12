using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TTMarket.Products.Application;
using TTMarket.Products.Application.Middleware;
using TTMarket.Products.Infrastructure;
using TTMarket.Products.Persistence;

Log.Logger = new LoggerConfiguration().WriteTo.Console()
                                      .CreateLogger();
try
{
    Log.Logger.Information("Application Starting");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
    builder.Services.AddPersistenceServices(builder.Configuration);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
                          builder => builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod());
    });
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

    var app = builder.Build();
    
    app.UseCors("CorsPolicy");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}