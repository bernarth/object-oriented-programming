using System.Text;
using BugTracker.Domain;

namespace BugTracker.Presentation;

public static class ConsoleReport
{
  public static void Print(IEnumerable<Bug> bugs)
  {
    foreach (var bug in bugs)
      Console.WriteLine(Format(bug));
  }

  public static string Format(Bug bug)
  {
    var builder = new StringBuilder();
    builder.Append('[')
           .Append(bug.Id)
           .Append("] ")
           .Append(bug.Title)
           .Append(" | ")
           .Append(bug.Severity)
           .Append(" | ")
           .Append(bug.IsFixed ? "Fixed" : "Open");

    return builder.ToString();
  }
}
