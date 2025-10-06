namespace TestDocCli.Errors;

public abstract class KnownUserErrorException : Exception
{
  public int ExitCode { get; }

  protected KnownUserErrorException(string message, int exitCode, Exception? inner = null) : base(message, inner)
  {
    ExitCode = exitCode;
  }
}
