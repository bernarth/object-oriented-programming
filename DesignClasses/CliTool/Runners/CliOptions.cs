namespace CliTool.Runners;

public sealed class CliOptions
{
  public bool ListTemplates { get; private set; }
  public string? TemplateId { get; private set; }
  public string? Name { get; private set; }
  public string? Purpose { get; private set; }
  public string? Suite { get; private set; }
  public string[]? Exporter { get; private set; }
  public string? OutPath { get; private set; }

  public static CliOptions Parse(string[] args)
  {
    var output = new CliOptions();

    // TODO: Enhance this if/else calls
    foreach (var argument in args)
    {
      const string TEMPLATE = "--template=";
      const string PURPOSE = "--purpose=";

      switch (argument)
      {
        case var a when a.Equals("--list", StringComparison.OrdinalIgnoreCase):
        output.ListTemplates = true;
        break;

        case var a when a.StartsWith(TEMPLATE, StringComparison.OrdinalIgnoreCase):
        output.TemplateId = a.Substring(TEMPLATE.Length);
        break;

        case var a when a.StartsWith(PURPOSE, StringComparison.OrdinalIgnoreCase):
        output.Purpose = a.Substring(PURPOSE.Length);
        break;
      }
    }
    
    return output;
  }
}