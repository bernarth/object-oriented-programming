using TestDocCli.Formatters;

namespace TestDocCli.AppCore;

// TODO: Add validation for the `formatArgument` (Could be here or in the Arguments class, or else where)
// The validation should valid if we don't have a default value. And display messages for valid inputs
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
      _ => new PlainConsoleFormatter()
    };
  }
}