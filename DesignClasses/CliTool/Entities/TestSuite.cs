namespace CliTool.Entities;

public sealed class TestSuite(string title)
{
  public string Title { get; } = title;
  public List<TestCase> Cases { get; set; } = [];
}