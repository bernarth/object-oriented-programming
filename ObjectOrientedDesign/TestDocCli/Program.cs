using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Extensions;

try
{
  HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

  builder.Services.Configure<AppSettings>(builder.Configuration);
  builder.Services.AddServices();
  builder.Services.AddErrorServices();
  builder.Services.AddHostedService<AppHostedService>();

  using IHost host = builder.Build();
  await host.RunAsync();
}
catch (KnownUserErrorException knownUserError)
{
  var reporter = new ErrorReporter();
  reporter.Write("ERROR", knownUserError.Message);
  Environment.ExitCode = (int)knownUserError.ExitCode;
}
catch (Exception exception)
{
  var reporter = new ErrorReporter();
  reporter.Write("UNEXPECTED", exception.Message);
  Environment.ExitCode = 1;
}

// We are defining the following
// WE have a IConsole where we can define the console operations for write and read
//  - The read opeartion might complex so, we created a different class that will implement IConsole (or not) to perform the read operation

// SOLVED: [UPDATE] Create the diagram of this project v2. It is saved on the files Updated-Diagram-Project-v2.drawio and Updated-Diagram-Project-v2.drawio.png
