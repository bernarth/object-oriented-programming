using System.Text;
using TestDocCli.Model;

namespace TestDocCli.Formatters;

public sealed class HtmlFormatter : IOutputFormatter
{
  public string FileExtension => "html";

  public string Format(TestDocument document)
  {
    StringBuilder builder = new();
    builder.AppendLine("<!doctype html>");
    builder.AppendLine("<html lang=\"en\">\n<head>\n<meta charset=\"utf-8\">\n<title>" + HtmlEscape(document.Title) + "</title>\n<style>body{font-family:system-ui,Segoe UI,Roboto,Helvetica,Arial,sans-serif;margin:24px;max-width:900px} ol{padding-left:1.25rem} .card{border:1px solid #ddd;border-radius:12px;padding:16px;margin:16px 0;box-shadow:0 1px 4px rgba(0,0,0,.06)} .muted{color:#666;font-size:.9rem}</style>\n</head>\n<body>");
    builder.AppendLine("<h1>" + HtmlEscape(document.Title) + "</h1>");
    builder.AppendLine("<p class=\"muted\">Created: " + document.CreatedAt.ToString("u") + "</p>");
    builder.AppendLine("<div class=\"card\"><p>" + HtmlEscape(document.Description) + "</p></div>");
    builder.AppendLine("<h2>Steps</h2>");
    builder.AppendLine("<ol>");


    for (int i = 0; i < document.Steps.Count; i++)
    {
      builder.AppendLine(" <li>" + HtmlEscape(document.Steps[i]) + "</li>");
    }

    builder.AppendLine("</ol>");
    builder.AppendLine("<div class=\"card\"><h3>Expected</h3><p>" + HtmlEscape(document.Expected) + "</p></div>");
    builder.AppendLine("<div class=\"card\"><h3>Actual</h3><p>" + HtmlEscape(document.Actual) + "</p></div>");
    builder.AppendLine("</body>\n</html>");

    return builder.ToString();
  }

  private static string HtmlEscape(string value)
  {
    return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
  }
}