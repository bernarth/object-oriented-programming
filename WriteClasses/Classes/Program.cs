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


// Tests for TestCase Implementation
// Test to create a TestCase Object with default Priority P3
var testCase = new TestCase("Hola Joanna", async () =>
{
  Console.Write("Fisrt step");
  await Task.Delay(10);
});
// Print title and default priority
Console.WriteLine($"title: {testCase.Title}");
Console.WriteLine($"Test priority: {testCase.Priority}");

// change priority
testCase.Priority = PriorityEnum.P4;
Console.WriteLine($"Test priority: {testCase.Priority}");
// print default status
Console.WriteLine($"Test status: {testCase.Status}");

// test the maxlength restrinction for the title
// testCase.Title = "really really really really really really really really really really really really really really really really really long title";
// Console.WriteLine($"title: {testCase.Title}");

// test to set a reason for failure
testCase.MarkFailed("failure reason for step");
Console.WriteLine($"reason: {testCase.FailureReason}");

// test the maxlength for reason
// testCase.MarkFailed("really really really really really really really really really really really really really really really really really really really really really really really really really really really long reason for failure");
// Console.WriteLine($"reason: {testCase.FailureReason}");

// test that null values can be set
testCase.MarkFailed(null);
Console.WriteLine($"reason: {testCase.FailureReason}");