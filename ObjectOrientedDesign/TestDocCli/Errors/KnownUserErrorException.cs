namespace TestDocCli.Errors;

public enum Codes
{
  InvalidConfiguration = 2,
  InvalidData = 3,
  ExportFailed = 4
}

public abstract class KnownUserErrorException : Exception
{
  public Codes ExitCode { get; }

  protected KnownUserErrorException(string message, Codes exitCode, Exception? inner = null) : base(message, inner)
  {
    ExitCode = exitCode;
  }
}
