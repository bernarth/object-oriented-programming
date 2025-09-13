using CliTool.Abstractions;

namespace CliTool.Entities;

public sealed class AssertStep(string title, string expectation) : TestStep(title)
{
  public string Expectation { get; } = expectation;

  public override string Describe() =>
    $"""
    Assert: {Title}
     - Instruction: {Expectation}
    """;
}