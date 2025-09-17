using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestSummary
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var options = CommandLineOptions.Parse(args);
      if (!File.Exists(options.FilePath))
      {
        Console.WriteLine("FILE_NOT_FOUND " + options.FilePath);
      }

      var rows = CsvReader.Read(options.FilePath);
      var summary = TestSummaryCalculator.Calculate(rows);

      Reporter.Print(options.FilePath, summary, options.Notify);
    }
  }

  public class CommandLineOptions
  {
    public string FilePath { get; private set; } = "test-results.csv";
    public bool Notify { get; private set; }

    public static CommandLineOptions Parse(string[] args)
    {
      var opts = new CommandLineOptions();
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i] == "--file" && i + 1 < args.Length)
        {
          opts.FilePath = args[i + 1];
        }

        if (args[i] == "--notify")
        {
          opts.Notify = true;
        }
      }
      return opts;
    }
  }

  public static class CsvReader
  {
    public static List<string[]> Read(string filePath)
    {
      var lines = File.ReadAllLines(filePath).ToList();
      var rows = new List<string[]>();
      for (int i = 0; i < lines.Count; i++)
      {
        var l = lines[i].Trim();
        if (l.Length == 0) continue;
        if (i == 0 && l.StartsWith("Suite,TestName,Status")) continue; // skip header
        var parts = l.Split(',');

        if (parts.Length < 5)
        {
          continue;
        }

        rows.Add(parts);
      }
      return rows;
    }
  }

  public class TestSummaryResult
  {
    public int Total { get; set; }
    public int Passed { get; set; }
    public int Failed { get; set; }
    public List<string> FailingTests { get; set; } = new();
  }

  public static class TestSummaryCalculator
  {
    public static TestSummaryResult Calculate(IEnumerable<string[]> rows)
    {
      int total =0, passed =0, failed = 0;
      var failingTests = new List<string>();
      var seenFail = new HashSet<string>();
      foreach (var r in rows)
      {
        total++;
        var suite = r[0].Trim();
        var test = r[1].Trim();
        var status = r[2].Trim().ToUpperInvariant();

        if (status == "PASS") passed++;
        else if (status == "FAIL")
        {
          failed++;
          var key = suite + "/" + test;

          if (!seenFail.Contains(key))
          {
            failingTests.Add(key);
            seenFail.Add(key);
          }
        }
      }
      return new TestSummaryResult
      {
        Total = total,
        Passed = passed,
        Failed = failed,
        FailingTests = failingTests.OrderBy(x=>x).ToList()
      };
    }
  }

  public static class Reporter
  {
    public static void Print(string filePath, TestSummaryResult summary, bool notify)
    {
      Console.WriteLine("==== Test Summary ====");
      Console.WriteLine("File: " + filePath);
      Console.WriteLine("Total: " + summary.Total);
      Console.WriteLine("Passed: " + summary.Passed);
      Console.WriteLine("Failed: " + summary.Failed);
      Console.WriteLine();
      Console.WriteLine("Failing Tests:");
      if (summary.FailingTests.Count == 0)
      {
        Console.WriteLine("(none)");
      }
      else
      {
        foreach (var t in summary.FailingTests)
        {
          Console.WriteLine("- " + t);
        }
      }
      if (notify)
      {
        Console.WriteLine();
        Console.WriteLine("NOTIFY => #qa-alerts | failed=" + summary.Failed + " | unique failing tests=" + summary.FailingTests.Count);
      }
    }
  }
}
