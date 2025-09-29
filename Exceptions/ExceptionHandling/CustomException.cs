namespace ExceptionHandling;

public sealed class CustomException : Exception
{
  public CustomException(string message) : base($"{DateTime.Now} {message}")
  {
    Console.WriteLine(message);
  }
}