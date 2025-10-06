namespace TestDocCli.Errors;

public interface IErrorReporter
{
  void Write(string category, string message);
}
