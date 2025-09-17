using CliTool.Interfaces;

namespace CliTool.Entities;

public sealed class ConsoleExporter : IExporter
{
  public void Export(TestCaseDocument document)
  {
    foreach (var section in document.Sections)
    {
      Console.WriteLine($"\n💡 {section.Title}\n{section.Content}");
    }
  }
}