using CliTool;
using CliTool.Entities;
using CliTool.Interfaces;
using CliTool.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// SOLVED: What is AddSingleton, AddScoped, AddTransient
// AddSingleton:  A single instance is created and shared through the entire application.
//                An example of use case: When only one shared instance is created to manage all requests.  
// AddScoped:     A new instance is created per HTTP request. All services within that request share the same instance.
//                An example of use case: When a per-request data, like database contexts is needed. 
// AddTransient:  A new instance is created every time the service IS REQUESTED.
//                An example of use case: Whe we need lightweight, stateless services.

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
