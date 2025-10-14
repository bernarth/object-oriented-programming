// using System.Text;
using BugTracker.Domain;

namespace BugTracker.Presentation;

public class ConsoleReport()
{
  public static void Print(IEnumerable<Bug> bugs)
  {
    foreach (Bug bug in bugs)
    {
      string line = ConsoleFormatter.Format(bug);
      Console.WriteLine(line);
    }
  }
}
