namespace CliTool.Entities;

public sealed class Template(string id, string description, Func<string, TestCase> factory)
{
  public string Id { get; } = id;
  public string Description { get; } = description;
  public Func<string, TestCase> Factory { get; } = factory;
}