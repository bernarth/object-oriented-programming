using TestSummary.Entities;

namespace TestSummary;

public class Reporter
{
  public void Report(Summary summary, List<string> failingTests, string filePath, bool notify)
  {
    Console.WriteLine("==== Test Summary ====");
    Console.WriteLine("File: " + filePath);
    Console.WriteLine("Total: " + summary.TotalTests);
    Console.WriteLine("Passed: " + summary.PassedTests);
    Console.WriteLine("Failed: " + summary.FailedTests);
    Console.WriteLine();
    Console.WriteLine("Failing Tests:");

    if (failingTests.Count == 0)
    {
      Console.WriteLine("(none)");
    }
    else
    {
      foreach (var t in failingTests.OrderBy(x => x))
      {
        Console.WriteLine("- " + t);
      }
    }

    if (notify)
    {
      Console.WriteLine();
      Console.WriteLine("NOTIFY => #qa-alerts | failed=" + summary.FailedTests + " | unique failing tests=" + failingTests.Count);
    }
  }
}