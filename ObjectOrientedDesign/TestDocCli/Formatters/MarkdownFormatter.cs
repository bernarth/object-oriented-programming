using System.Text;
using TestDocCli.Model;

namespace TestDocCli.Formatters;

public sealed class MarkdownFormatter : IOutputFormatter
{
  public string FileExtension => "md";

  public string Format(TestDocument document)
  {
    var builder = new StringBuilder();
    builder.AppendLine($"# {Escape(document.Title)}");
    builder.AppendLine();
    builder.AppendLine($"_Created: {document.CreatedAt.ToString("d")}");
    builder.AppendLine($"{Escape(document.Description)}");
    builder.AppendLine();
    builder.AppendLine("## Steps");

    for (int i = 0; i < document.Steps.Count; i++)
    {
      builder.AppendLine($"{i + 1}. {Escape(document.Steps[i])}");
    }

    builder.AppendLine();
    builder.AppendLine($"## ✅ Expected");
    builder.AppendLine();
    builder.AppendLine(Escape(document.Expected));
    builder.AppendLine();
    builder.AppendLine($"## ❌ Actual");
    builder.AppendLine();
    builder.AppendLine(Escape(document.Actual));
    builder.AppendLine();

    return builder.ToString();
  }

  private static string Escape(string value)
  {
    return value.Replace("<", "&lt;").Replace(">", "&gt;");
  }
}