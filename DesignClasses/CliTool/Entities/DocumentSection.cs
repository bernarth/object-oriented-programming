namespace CliTool.Entities;

public sealed class DocumentSection(string title, string content)
{
  public string Title { get; } = title;
  public string Content { get; } = content;
}