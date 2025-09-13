using CliTool.Abstractions;

namespace CliTool.Entities;

public sealed class ActionStep(string title, string instruction) : TestStep(title)
{
  public string Instruction { get; } = instruction;

  public override string Describe() =>
    $"""
    Action: {Title}
     - Instruction: {Instruction}
    """;
}