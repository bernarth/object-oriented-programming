using TestDocCli.InputOutput;
using TestDocCli.Model;

namespace TestDocCli.AppCore;

public class PromptFlow(IInputReader inputReader, IConsole console) : IPromptFlow
{
  private readonly IInputReader _inputReader = inputReader ?? throw new ArgumentNullException();
  private readonly IConsole _console = console ?? throw new ArgumentNullException();

  public TestDocument CollectTestDocument()
  {
    _console.WriteLine("Test Documentation Generator\n");

    string title = _inputReader.ReadRequired("Title");
    string description = _inputReader.ReadRequired("Description");

    List<string> steps = [];
    _console.WriteLine("Enter Steps (one per line). Submit an empty line to finish:");

    int stepNumber = 1;

    while (true)
    {
      string prompt = $"Step {stepNumber}: ";
      string line = _inputReader.ReadLine(prompt);

      if (string.IsNullOrWhiteSpace(line))
      {
        break;
      }

      steps.Add(line.Trim());
      stepNumber++;
    }

    string expected = _inputReader.ReadRequired("Expected Result");
    string actual = _inputReader.ReadRequired("Actual Result");

    return new TestDocument
    {
      Title = title.Trim(),
      Description = description.Trim(),
      Steps = steps,
      Expected = expected.Trim(),
      Actual = actual.Trim(),
      CreatedAt = DateTimeOffset.Now
    };
  }
}