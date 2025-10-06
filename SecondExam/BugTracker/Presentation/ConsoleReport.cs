using System.Text;
using BugTracker.Domain;

namespace BugTracker.Presentation;

public class ConsoleReport
{
  public static void Print(IEnumerable<Bug> bugs)
  {
    foreach (Bug bug in bugs)
    {
      string line = Format(bug);
      Console.WriteLine(line);
    }
  }

  public static string Format(Bug bug)
  {
    StringBuilder builder = new StringBuilder();
    builder.Append('[')
           .Append(bug.Id)
           .Append("] ")
           .Append(bug.Title)
           .Append(" | ")
           .Append(bug.Severity.ToString())
           .Append(" | ")
           .Append(bug.IsFixed ? "Fixed" : "Open");

    return builder.ToString();
  }
}
