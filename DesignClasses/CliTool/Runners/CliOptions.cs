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
      if (argument.Equals("--list", StringComparison.OrdinalIgnoreCase))
      {
        output.ListTemplates = true;
      }
      else if (argument.StartsWith("--template=", StringComparison.OrdinalIgnoreCase))
      {
        output.TemplateId = argument.Split('=', 2)[1];
      }
      else if (argument.StartsWith("--purpose=", StringComparison.OrdinalIgnoreCase))
      {
        output.Purpose = argument.Split('=', 2)[1];
      }
    }

    return output;
  }
}