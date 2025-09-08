namespace Classes;

public class LoginPageBad
{
  private readonly SeleniumDriver _driver = new();

  public void Login(string username, string password)
  {
    _driver.EnterText("#username", username);
    _driver.EnterText("#password", password);
    _driver.Click("#loginButton");
  }

  public void GenerateReport()
    => Console.WriteLine("Generating report...");

  public void SaveTestDataToDatabase()
    => Console.WriteLine("Saving data into DB from login logs...");

  private class SeleniumDriver
  {
    public void EnterText(string elementId, string value)
    {
    }

    public void Click(string actionId)
    {
    }
  }
}

// Problems
// - LoginPage handles reporting and batabase work => low cohesion
// - It is hardcoded to use SeleniumDriver => tight coupling
// - If you want to test with Playwright, you must rewrite this class.