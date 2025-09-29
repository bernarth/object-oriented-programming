using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestDocCli.AppCore;
using TestDocCli.InputOutput;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Bind simple options from command-line
builder.Services.Configure<AppSettings>(builder.Configuration);

// Core services
builder.Services.AddSingleton<IConsole, SystemConsole>();
builder.Services.AddSingleton<IInputReader, ConsoleInputReader>();
builder.Services.AddSingleton<IOutputFormatterFactory, OutputFormatterFactory>();
builder.Services.AddSingleton<IPromptFlow, PromptFlow>();
builder.Services.AddSingleton<IFileExporter, FileExporter>();

// App host
builder.Services.AddHostedService<AppHostedService>();

using IHost host = builder.Build();
await host.RunAsync();

// We are defining the following
// WE have a IConsole where we can define the console operations for write and read
//  - The read opeartion might complex so, we created a different class that will implement IConsole (or not) to perform the read operation

// TODO: Create the diagram of this project.
