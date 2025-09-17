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

    var optionSetters = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase)
    {
      ["--template"] = val => output.TemplateId = val,
      ["--name"] = val => output.Name = val,
      ["--purpose"] = val => output.Purpose = val,
      ["--suite"] = val => output.Suite = val,
      ["--exporter"] = val => output.Exporter = val.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
      ["--out"] = val => output.OutPath = val
    };

    foreach (var argument in args)
    {
      if (argument.Equals("--list", StringComparison.OrdinalIgnoreCase))
      {
        output.ListTemplates = true;
        continue;
      }

      var parts = argument.Split('=', 2);
      if (parts.Length != 2)
        continue;

      var key = parts[0];
      var value = parts[1];

      if (optionSetters.TryGetValue(key, out var setter))
      {
        setter(value);
      }
      else
      {
        Console.WriteLine($"Advertencia: argumento no reconocido '{key}'");
      }
    }
    return output;
  }
}
