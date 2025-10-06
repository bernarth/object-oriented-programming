using Microsoft.Extensions.DependencyInjection;
using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.InputOutput;

namespace TestDocCli.Extensions;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddSingleton<IConsole, SystemConsole>();
    services.AddSingleton<IInputReader, ConsoleInputReader>();
    services.AddSingleton<IOutputFormatterFactory, OutputFormatterFactory>();
    services.AddSingleton<IPromptFlow, PromptFlow>();
    services.AddSingleton<IFileExporter, FileExporter>();

    return services;
  }

  public static IServiceCollection AddErrorServices(this IServiceCollection services)
  {
    services.AddSingleton<IErrorReporter, ErrorReporter>();

    return services;
  }
}
