namespace TestDocCli.Errors;

public class ErrorReporter : IErrorReporter
{
  public void Write(string category, string message)
  {
    Console.Error.WriteLine($"[{category}]: {message}");
  }
}
