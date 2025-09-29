namespace TestSummary.Entities;

public class Summary
{
  public required int TotalTests { get; init; }
  public required int PassedTests { get; init; }
  public required int FailedTests { get; init; }

  public override string ToString()
  {
    return $"Total Tests: {TotalTests}, Passed: {PassedTests}, Failed: {FailedTests}";
  }
}