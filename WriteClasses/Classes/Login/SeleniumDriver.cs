namespace Classes.Login;

public class SeleniumDriver : IBrowserDriver
{
  public void Click(string selector) =>
    Console.WriteLine($"Clicking on {selector}");

  public void EnterText(string selector, string text) =>
    Console.WriteLine($"Typing {text} into {selector}");

  public void NavigateTo(string url) =>
    Console.WriteLine($"Navigating to {url}");
}