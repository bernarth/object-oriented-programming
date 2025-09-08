namespace Classes.Login;

public interface IBrowserDriver
{
  void NavigateTo(string url);
  void Click(string selector);
  void EnterText(string selector, string text);
}