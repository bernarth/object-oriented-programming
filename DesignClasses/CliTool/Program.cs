using CliTool;
using CliTool.Entities;
using CliTool.Interfaces;
using CliTool.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;




var builder = Host.CreateApplicationBuilder(args);

// SOLVED: What is AddSingleton, AddScoped, AddTransient
// They are methods used to register services with different lifetimes, they determine how 
// instances of a service are created amd managed by the dependency injection container
// AddSingleton: The services are creted only once per application. The same instance of the service 
//               is shared across all requests and throughout the entire application lifetime
// AddScoped:    The services are created once per scope (each new HTTP request). Anew instance of the service
//               is created for each new scope, but within that scope the same instance is reused for subsequent
//               requests for that service 
// AddTransient: This services are created every time they are requested. A new instance of the service is 
//               created each time it is injected even within the same scope

builder.Services.AddSingleton(DemoTemplates.Library);
builder.Services.AddSingleton<TestCaseBuilder>();

builder.Services.AddSingleton<IExporter, ConsoleExporter>();
builder.Services.AddSingleton<IExporter>(sp =>
{
  var outArg = Environment.GetCommandLineArgs().FirstOrDefault(a =>
    a.StartsWith("--out=", StringComparison.OrdinalIgnoreCase));
  var path = outArg is null ? "testcase.md" : outArg.Split('=', 2)[1];

  return new MarkdownFileExporter(path);
});

builder.Services.AddSingleton<CliRunner>();

using var host = builder.Build();

var runner = host.Services.GetRequiredService<CliRunner>();
var exit = runner.Run(args);

Environment.ExitCode = exit;
