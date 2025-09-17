using CliTool.Abstractions;

namespace CliTool.Entities;

public sealed class TestCase(string name, string purpose, List<TestStep> steps)
{
  public string Name { get; } = name;
  public string Purpose { get; } = purpose;
  public List<TestStep> Steps { get; } = steps;
}