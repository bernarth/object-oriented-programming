namespace TestDocCli.Model;

public sealed class TestDocument
{
  public required string Title { get; init; }
  public required string Description { get; init; }
  public required List<string> Steps { get; init; }
  public required string Expected { get; init; }
  public required string Actual { get; init; }
  public required DateTimeOffset CreatedAt { get; init; }
}