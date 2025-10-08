namespace TestLogs;

public abstract class TestCase
{
  public string Name { get; }
  public string Environment { get; }

  protected TestCase(string name, string environment)
  {
    Name = name;
    Environment = environment;
  }

  public virtual void Execute()
  {
    Console.WriteLine($"Executing test '{Name}' on environment '{Environment}'");
  }
}
