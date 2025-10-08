namespace TestLogs;

public class Logger
{
  public void LogInfo(string message)
  {
    Console.WriteLine($"[INFO]: {message}");
  }

  public void LogError(string message)
  {
    Console.WriteLine($"[ERROR]: {message}");
  }
}
