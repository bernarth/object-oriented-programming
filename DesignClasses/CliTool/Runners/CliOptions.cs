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

    // SOLVED: Enhance this if/else calls. I use a dictionary althoug it can be improved. 
    var options = new Dictionary<string, Action<string>>
    {
      ["--list"] = (value) => output.ListTemplates = true,
      ["--template"] = (value) => output.TemplateId = value,
      ["--purpose"] = (value) => output.Purpose = value,
    };
    foreach (var argument in args)
    {
      // for flags
      if (options.ContainsKey(argument.ToLowerInvariant()))
      {
        options[argument]("");
      }
      // for options with values
      else if (argument.Contains("="))
      {
        string option = argument.Split('=', 2)[0];
        string value = argument.Split('=', 2)[1];
        if (options.ContainsKey(option.ToLowerInvariant()))
        {
          options[option](value);
        }
      }
    }
    return output;
  }
}