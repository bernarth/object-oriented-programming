namespace TestLogs;

public class WebUiTest(string name, string environment, string browser) : TestCase(name, environment)
{
  public string Browser { get; } = browser;

  public override void Execute()
  {
    Console.WriteLine($"Launching {Browser} browser...");
    base.Execute();
    Console.WriteLine($"Running UI validation steps...");
  }
}
