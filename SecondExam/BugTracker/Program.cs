using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Extension methods for organized DI registration
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
host.Run();

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IBugRepository, BugRepository>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<BugTrackerService>();
        return services;
    }
}