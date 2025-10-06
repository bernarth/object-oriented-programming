using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// SOLVED: Enhance the DI -> Implemented the interfaces IBugRepository and IBugTrackerService.
builder.Services.AddSingleton<IBugRepository, BugRepository>();
builder.Services.AddSingleton<IBugTrackerService, BugTrackerService>();

builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
host.Run();