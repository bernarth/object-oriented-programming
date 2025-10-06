using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// TODO: Enhance the DI
builder.Services.AddSingleton<BugRepository>();
builder.Services.AddSingleton<BugTrackerService>();

builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
host.Run();