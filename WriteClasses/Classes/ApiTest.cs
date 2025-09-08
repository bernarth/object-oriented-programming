namespace Classes;

public class ApiTest
{
  /// <summary>
  /// public:
  /// Where it can be applied: classes, methods, field, properties
  /// Who can access: Accesible everywhere
  /// </summary>
  public string BaseUrl { get; set; } = "https://api.company.com/api/v1";

  /// <summary>
  /// private:
  /// Where it can be applied: Members (be aware when applying to classes)
  /// Who can access: Only inside the same class
  /// </summary>
  private string _authToken = string.Empty;

  /// <summary>
  /// protected:
  /// Where it can be applied: Members
  /// Who can access: Same class and derived classes
  /// </summary>
  /// <param name="user"></param>
  /// <param name="password"></param>
  protected void Authenticate(string user, string password)
  {
    _authToken = $"{user}:{password}-token";
  }

  /// <summary>
  /// internal:
  /// Where it can be applied: classes, methods, field, properties
  /// Who can access: Anywhere in the same assembly (project)
  /// e.g. helper methods, classes that are meant to work inside the project
  /// keyword: +
  /// </summary>
  /// <param name="endpoint"></param>
  internal void LogRequest(string endpoint)
  {
    Console.WriteLine($"Request sent to {endpoint}");
  }

  /// <summary>
  /// protected internal:
  /// Where it can be applied: Members
  /// Who can access: Same assembly (project) or derived classes
  /// Keyword: #
  /// </summary>
  /// <param name="response"></param>
  protected internal void LogReponse(string response)
  {
    Console.WriteLine($"Response: {response}");
  }

  /// <summary>
  /// private protected:
  /// Where it can be applied: Members
  /// Who can access: Same classes or derived classes inside the same assembly
  /// Keyword: #
  /// </summary>
  private protected void Log()
  {
    Console.WriteLine($"Run on {BaseUrl}");
  }
}