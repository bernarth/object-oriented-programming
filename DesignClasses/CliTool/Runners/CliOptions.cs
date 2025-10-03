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

    var actions = new Dictionary<string, Action<string>>
    {
      ["--template"] = val => output.TemplateId = val,
      ["--name"] = val => output.Name = val,
      ["--purpose"] = val => output.Purpose = val,
      ["--suite"] = val => output.Suite = val,
      ["--export"] = val => output.Exporter = val.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
      ["--out"] = val => output.OutPath = val
    };

    // SOLVE: Enhance this if/else calls
    foreach (var argument in args)
    {
      if (argument.Equals("--list", StringComparison.OrdinalIgnoreCase))
      {
        output.ListTemplates = true;
      }
      else if (argument.Contains('='))
      {
        var parts = argument.Split('=', 2);
        var key = parts[0].ToLowerInvariant();
        var value = parts[1];
        if (actions.TryGetValue(key, out var action))
        {
          action(value);
        }
      }
    }

    return output;
  }
}