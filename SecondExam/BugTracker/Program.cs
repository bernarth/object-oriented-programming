using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// TODO: Enhance the DI
builder.Services.AddSingleton<IRepository, BugRepository>();
builder.Services.AddSingleton<ITrackerService,BugTrackerService>();

builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
host.Run();