using BugTracker.Application;
using BugTracker.Hosting;
using BugTracker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IBugRepository, BugRepository>();
builder.Services.AddSingleton<BugTrackerService>();
builder.Services.AddHostedService<AppHostedService>();

using var host = builder.Build();
host.Run();
