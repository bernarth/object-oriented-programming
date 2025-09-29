namespace TestDocCli.InputOutput;

public sealed class ConsoleInputReader(IConsole console) : IInputReader
{
  private readonly IConsole _console = console ?? throw new ArgumentNullException();

  public string ReadRequired(string label)
  {
    while (true)
    {
      _console.Write($"{label}: ");
      string input = _console.ReadLine();

      if (!string.IsNullOrWhiteSpace(input))
      {
        return input;
      }

      _console.WriteLine($"{label} cannot be empty. Please try again.");
    }
  }

  public string ReadLine(string prompt)
  {
    _console.Write(prompt);
    string input = _console.ReadLine();

    return input;
  }
}