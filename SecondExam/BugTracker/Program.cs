using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using BugTracker.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// TODO: Enhance the DI
builder.Services.AddSingleton<IBugRepository, BugRepository>();
builder.Services.AddSingleton<IBugTrackerService, BugTrackerService>();

builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
host.Run();