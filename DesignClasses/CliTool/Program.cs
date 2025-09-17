using CliTool;
using CliTool.Entities;
using CliTool.Interfaces;
using CliTool.Runners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// TODO: What is AddSingleton, AddScoped, AddTransient
// AddSingleton: 
// AddScoped:
// AddTransient:

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
