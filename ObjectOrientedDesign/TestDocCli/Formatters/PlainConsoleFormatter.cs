using System.Text;
using TestDocCli.Model;

namespace TestDocCli.Formatters;

public sealed class PlainConsoleFormatter : IOutputFormatter
{
  public string FileExtension => "txt";

  public string Format(TestDocument document)
  {
    var builder = new StringBuilder();
    builder.AppendLine("============ TEST DOCUMENT ============");
    builder.AppendLine($"Title: {document.Title}");
    builder.AppendLine($"Description: {document.Description}");
    builder.AppendLine($"Created: {document.CreatedAt.ToString("d")}");
    builder.AppendLine();
    builder.AppendLine("Steps:");

    for (int i = 0; i < document.Steps.Count; i++)
    {
      builder.AppendLine($" {i + 1}. {document.Steps[i]}");
    }

    builder.AppendLine();
    builder.AppendLine($"✅ Expected: {document.Expected}");
    builder.AppendLine($"❌ Actual: {document.Actual}");
    builder.AppendLine("============ ============ ============");

    return builder.ToString();
  }
}