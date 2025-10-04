using TestDocCli.Formatters;

namespace TestDocCli.AppCore;

// SOLVED: Add validation for the `formatArgument` (Could be here or in the Arguments class, or else where)
// The validation should valid if we don't have a default value. And display messages for valid inputs
// Since the validation is for when we do not have a default value the exception would be thrown on the switch
public class OutputFormatterFactory : IOutputFormatterFactory
{
  public IOutputFormatter Create(string formatArgument)
  {
    string normalized = (formatArgument ?? string.Empty).Trim().ToLowerInvariant();

    return normalized switch
    {
      "md" or "markdown" => new MarkdownFormatter(),
      "html" => new HtmlFormatter(),
      "console" or "text" or "" => new PlainConsoleFormatter(),
      _ => throw new FormatException($"Unknown format {formatArgument}. Default format was not defined. Valid formats are: md or markdown, html, console or text")
    };
  }
}