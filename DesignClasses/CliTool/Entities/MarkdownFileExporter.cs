using CliTool.Interfaces;

namespace CliTool.Entities;

public sealed class MarkdownFileExporter(string path = "testcase.md") : IExporter
{
  private readonly string _path = path;

  public void Export(TestCaseDocument document)
  {
    using var streamWriter = new StreamWriter(_path, false);

    foreach (var section in document.Sections)
    {
      streamWriter.WriteLine($"# {section.Title}");
      streamWriter.WriteLine(section.Content);
      streamWriter.WriteLine();
    }
  }
}