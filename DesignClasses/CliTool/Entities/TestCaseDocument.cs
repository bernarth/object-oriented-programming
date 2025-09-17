namespace CliTool.Entities;

public sealed class TestCaseDocument
{
  private readonly List<DocumentSection> _sections = [];
  public IEnumerable<DocumentSection> Sections => _sections;

  public void AddSection(string title, string content) =>
    _sections.Add(new DocumentSection(title, content));
}