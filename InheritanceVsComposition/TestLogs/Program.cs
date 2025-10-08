using TestLogs;

// Inheritance
Console.WriteLine("Inheritance");

TestCase uiTest = new WebUiTest("Login page", "QA", "Chrome");
TestCase apiTest = new ApiTest("Login API test", "QA", "/api/login");

uiTest.Execute();
Console.WriteLine();
apiTest.Execute();

// Composition
Console.WriteLine();
Console.WriteLine("Composition");
var testCase = new InfoTestCase("Login test", "DEV");

testCase.Execute();