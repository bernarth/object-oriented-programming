using CliTool;
using CliTool.Entities;
using CliTool.Interfaces;
using CliTool.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// TODO: AddSingleton: This method registers a Service as a Singleton service.
//    It means that the service is created once per application and the 
//    same instance is used all over the app. E.g: Logging
// AddScoped: With this method a new instance is created but once for 
//     each specific request. This ensures that all components in the 
//     same request share the same instance.
// AddTransient: with this method a new instance of the service is created
//     each time it is requested.

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
