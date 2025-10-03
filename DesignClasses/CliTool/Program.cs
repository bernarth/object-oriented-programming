using CliTool;
using CliTool.Entities;
using CliTool.Interfaces;
using CliTool.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// SOLVED: What is AddSingleton, AddScoped, AddTransient
// AddSingleton: Instance created: Once | Lifetime: Application
//    * Register a service as a singleton
//    * Only one instance is created and shared for all the application
// AddScoped: Instance created: Once per scope | Lifetime: Scope (e.g. request)
//    * Register a service as scoped
//    * A new instance is created per scope
// AddTransient: Instance created: Every request | Lifetime: None
//    * Registers a service as transient
//    * A new instance is created every time the service is requested

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
