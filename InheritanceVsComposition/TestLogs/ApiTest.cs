namespace TestLogs;

public class ApiTest(string name, string environment, string endpoint) : TestCase(name, environment)
{
  public string Endpoint { get; } = endpoint;

  public override void Execute()
  {
    Console.WriteLine($"Calling API endpoint {Endpoint}...");
    base.Execute();
    Console.WriteLine($"Verifying API response...");
  }
}
