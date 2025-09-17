using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var testApp = new TestSummaryApp();
testApp.Run(args);

public class TestSummaryApp
{
  private readonly TestResultFileParser _parser;
  private readonly TestResultsAnalyzer _analyzer;
  private readonly ResultsReporter _reporter;

  public TestSummaryApp()
  {
    _parser = new TestResultFileParser();
    _analyzer = new TestResultsAnalyzer();
    _reporter = new ResultsReporter();
  }

  public void Run(string[] args)
  {
    var (filePath, notify) = ParseCommandLineArgs(args);

    if (!File.Exists(filePath))
    {
      Console.WriteLine("FILE_NOT_FOUND " + filePath);
      return;
    }

    try
    {
      var lines = File.ReadAllLines(filePath);
      var testResults = _parser.ParseCsv(lines);
      _analyzer.Analyze(testResults);
      _reporter.PrintSummary(filePath, _analyzer, notify);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error processing file: {ex.Message}");
    }
  }

  private (string filePath, bool notify) ParseCommandLineArgs(string[] args)
  {
    string filePath = "test-results.csv";
    bool notify = false;

    for (int i = 0; i < args.Length; i++)
    {
      if (args[i] == "--file" && i + 1 < args.Length)
      {
        filePath = args[i + 1];
      }

      if (args[i] == "--notify")
      {
        notify = true;
      }
    }

    return (filePath, notify);
  }
}

public class TestResult
{
  public string? Suite { get; set; }
  public string? TestName { get; set; }
  public string? Status { get; set; }
  public string? Duration { get; set; }
  public string? Timestamp { get; set; }
  public string UniqueKey => $"{Suite}/{TestName}";
}

public class TestResultFileParser
{
  public List<TestResult> ParseCsv(string[] lines)
  {
    var results = new List<TestResult>();

    for (int i = 0; i < lines.Length; i++)
    {
      var line = lines[i].Trim();
      if (line.Length == 0) continue;
      if (i == 0 && line.StartsWith("Suite,TestName,Status")) continue;

      var parts = line.Split(',');
      if (parts.Length < 5) continue;

      results.Add(new TestResult
      {
        Suite = parts[0].Trim(),
        TestName = parts[1].Trim(),
        Status = parts[2].Trim().ToUpperInvariant(),
        Duration = parts[3].Trim(),
        Timestamp = parts[4].Trim(),
      });
    }

    return results;
  }
}

public class TestResultsAnalyzer
{
  public int Total { get; set; }
  public int Passed { get; set; }
  public int Failed { get; set; }
  public List<string> FailingTests { get; private set; } = new List<string>();

  public void Analyze(List<TestResult> results)
  {
    Total = results.Count;
    Passed = results.Count(result => result.Status == "PASS");
    Failed = results.Count(result => result.Status == "FAIL");

    var seenFail = new HashSet<string>();
    FailingTests = results
      .Where(result => result.Status == "FAIL")
      .Select(result => result.UniqueKey)
      .Where(key => seenFail.Add(key))
      .OrderBy(key => key)
      .ToList();
  }
}

public class ResultsReporter
{
  public void PrintSummary(string filePath, TestResultsAnalyzer analyzer, bool notify)
  {
    Console.WriteLine("==== Test Summary ====");
    Console.WriteLine("File: " + filePath);
    Console.WriteLine("Total: " + analyzer.Total);
    Console.WriteLine("Passed: " + analyzer.Passed);
    Console.WriteLine("Failed: " + analyzer.Failed);
    Console.WriteLine();
    Console.WriteLine("Failing Tests:");

    if (analyzer.FailingTests.Count == 0)
    {
      Console.WriteLine("(none)");
    }
    else
    {
      foreach (var test in analyzer.FailingTests)
      {
        Console.WriteLine("- " + test);
      }
    }

    if (notify)
    {
      Console.WriteLine();
      Console.WriteLine("NOTIFY => #qa-alerts | failed=" + analyzer.Failed +
                       " | unique failing tests=" + analyzer.FailingTests.Count);
    }
  }
}