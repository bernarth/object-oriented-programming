namespace TestLogs;

public class InfoTestCase(string name, string environment)
{
  private readonly Logger _logger = new();
  private readonly Notifier _notifier = new();

  public string Name { get; } = name;
  public string Environment { get; } = environment;

  public void Execute()
  {
    _logger.LogInfo("Start test steps...");
    Console.WriteLine($"Executing test '{Name}' on environment '{Environment}'");
    _logger.LogInfo("Test finished...");
    _notifier.Notify($"Test '{Name}' finsihed on environment '{Environment}'");
  }
}
