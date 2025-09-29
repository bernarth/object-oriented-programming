namespace TestDocCli.AppCore;

public sealed class AppSettings
{
  public string Format { get; init; } = "console";
  public string OutputDirectory { get; init; } = string.Empty;
}
