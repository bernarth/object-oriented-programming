
using DiLifeTime.AppCore;
using DiLifeTime.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Register our services with different lifetimes using Dependency Injection
builder.Services.AddSingleton<NumberStorageSingleton>();
builder.Services.AddScoped<NumberStorageScoped>();
builder.Services.AddTransient<NumberStorageTransient>();

// Use our own hosted service
builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
await host.RunAsync();