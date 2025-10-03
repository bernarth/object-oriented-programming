using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TestDocCli.Errors;
using TestDocCli.Formatters;
using TestDocCli.InputOutput;
using TestDocCli.Model;

namespace TestDocCli.AppCore;

public class AppHostedService(
  IOptions<AppSettings> settings,
  IConsole console,
  IPromptFlow promptFlow,
  IOutputFormatterFactory formatterFactory,
  IFileExporter fileExporter,
  IErrorReporter errorReporter) : IHostedService
{
  private readonly IOptions<AppSettings> _settings = settings;
  private readonly IConsole _console = console;
  private readonly IPromptFlow _promptFlow = promptFlow;
  private readonly IOutputFormatterFactory _formatterFactory = formatterFactory;
  private readonly IFileExporter _fileExporter = fileExporter;
  private readonly IErrorReporter _errorReporter = errorReporter;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    try
    {
      string format = _settings.Value.Format;
      IOutputFormatter formatter = _formatterFactory.Create(format);

      TestDocument testDocument = _promptFlow.CollectTestDocument();
      string output = formatter.Format(testDocument);

      _console.WriteLine(output);

      string directory = string.IsNullOrWhiteSpace(_settings.Value.OutputDirectory)
        ? Directory.GetCurrentDirectory()
        : _settings.Value.OutputDirectory;

      // SOLVED: replace string.Empty with testDocument.Title
      string savedPath = _fileExporter.Save(output, formatter.FileExtension, testDocument.Title ?? "Untitled", directory);
      _console.WriteLine($"Saved to: {savedPath}");
    }
    catch (KnownUserErrorException knownUserError)
    {
      _errorReporter.Write("ERROR", knownUserError.Message);
      Environment.ExitCode = knownUserError.ExitCode;
    }
    catch (Exception exception)
    {
      _errorReporter.Write("UNEXPECTED", exception.Message);
      Environment.ExitCode = 1;
    }

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _console.WriteLine("TestDocCli finished...");
    return Task.CompletedTask;
  }
}
