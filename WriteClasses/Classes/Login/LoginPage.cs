namespace Classes.Login;

public class LoginPage(IBrowserDriver driver)
{
  private readonly IBrowserDriver _driver = driver ?? throw new ArgumentNullException();

  public void Login(string username, string password)
  {
    _driver.EnterText("#username", username);
    _driver.EnterText("#password", password);
    _driver.Click("#loginButton");
  }
}