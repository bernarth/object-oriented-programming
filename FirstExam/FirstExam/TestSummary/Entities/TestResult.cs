namespace TestSummary.Entities;

public class TestResult
{
  public required string Suite { get; init; }
  public required string TestName { get; init; }
  public required Status Status { get; init; }
  public required int DurationMs { get; init; }
  public required DateTime Timestamp { get; init; }
}