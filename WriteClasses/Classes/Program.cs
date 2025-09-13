using Classes.Login;
// BAD: Using just variables without a class
using Classes;

//int testId = 1;
string testName = "Login with valid credentials";
IEnumerable<string> testSteps =
[
  "1. Open App",
  "2. Enter username",
  "3. Enter password"
];
Console.WriteLine($"Executing {testName}");

// Problems:
// - Code duplication
// - No structure (everything is loose variables)
// - Hard to maintain

var test = new ApiTest();

// Having more cohesion and less coupling

var loginPage = new LoginPage(new SeleniumDriver());
loginPage.Login("user", "secret");
