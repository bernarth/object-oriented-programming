using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Intentionally tiny and messy; students will refactor to OOP.
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

if (!File.Exists(filePath))
{
  Console.WriteLine("FILE_NOT_FOUND " + filePath);
  return;
}

// Globals / shared mutable state (bad on purpose)
var lines = File.ReadAllLines(filePath).ToList();
var testResults = new List<TestResult>();

// Poor man's CSV (no quoting, no culture handling)
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

  testResults.Add(new TestResult
  {
    Suite = parts[0].Trim(),
    TestName = parts[1].Trim(),
    Status = parts[2].Trim(),
    Duration = parts[3].Trim(),
    Timestamp = parts[4].Trim(),
  });
}

int total = 0;
int passed = 0;
int failed = 0;
var failingTests = new List<string>();
var seenFail = new HashSet<string>();

// Mix parsing, counting, and output concerns in one place
foreach (var result in testResults)
{
  total++;

  if (result.Status == "PASS") passed++;
  else if (result.Status == "FAIL")
  {
    failed++;

    if (!seenFail.Contains(result.UniqueKey))
    {
      failingTests.Add(result.UniqueKey);
      seenFail.Add(result.UniqueKey);
    }
  }
}

Console.WriteLine("==== Test Summary ====");
Console.WriteLine("File: " + filePath);
Console.WriteLine("Total: " + total);
Console.WriteLine("Passed: " + passed);
Console.WriteLine("Failed: " + failed);
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
  Console.WriteLine("NOTIFY => #qa-alerts | failed=" + failed + " | unique failing tests=" + failingTests.Count);
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