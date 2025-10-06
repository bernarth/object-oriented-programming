namespace TestDocCli.Errors;

public enum ErrorCodes
{
  UnknownError = 1,
  ConfigurationError = 2,
  ValidationError = 3,
  ExportError = 4
}

public abstract class KnownUserErrorException : Exception
{
  public int ExitCode { get; }

  protected KnownUserErrorException(string message, int exitCode, Exception? inner = null) : base(message, inner)
  {
    ExitCode = exitCode;
  }
}
