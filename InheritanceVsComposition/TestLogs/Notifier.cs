namespace TestLogs;

public class Notifier
{
  public void Notify(string message)
  {
    Console.WriteLine($"[NOTIFY]: {message}");
  }
}
