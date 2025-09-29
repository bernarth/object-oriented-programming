namespace TestDocCli.InputOutput;

public sealed class SystemConsole : IConsole
{
  public void Write(string message)
  {
    Console.Write(message);
  }

  public void WriteLine(string message)
  {
    Console.WriteLine(message);
  }

  public string ReadLine()
  {
    string? input = Console.ReadLine();

    return input ?? string.Empty;
  }
}