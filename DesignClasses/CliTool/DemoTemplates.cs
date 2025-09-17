using CliTool.Entities;

namespace CliTool;

public static class DemoTemplates
{
  public static TemplateLibrary Library { get; } = new TemplateLibrary()
    .Add(new Template(
      id: "web-login",
      description: "Browser login flow (action + assert steps)",
      factory: name =>
      {
        var testCase = new TestCase(name, "Verify that a valid user can log in and land on the home page.", []);
        testCase.Steps.Add(new ActionStep("Open login page", "Navigate to /login"));
        testCase.Steps.Add(new ActionStep("Enter credentials", "Type valid username and password"));
        testCase.Steps.Add(new ActionStep("Submit", "Click the 'Sign in' button"));
        testCase.Steps.Add(new AssertStep("Home is visible", "Home title and user avatar are displayed"));
        return testCase;
      }))
    .Add(new Template(
      id: "api-health",
      description: "API health check (action + assert steps)",
      factory: name =>
      {
        var testCase = new TestCase(name, "Validate the API health endpoint reports 'up'.", []);
        testCase.Steps.Add(new ActionStep("Call /health", "Send GET /health"));
        testCase.Steps.Add(new AssertStep("Status code", "Response status is 200"));
        testCase.Steps.Add(new AssertStep("Payload", "JSON contains { \"status\": \"up\" }"));
        return testCase;
      }))
    .Add(new Template(
      id: "search",
      description: "Application search returns results",
      factory: name =>
      {
        var testCase = new TestCase(name, "Ensure searching by keyword returns at least one relevant result.", []);
        testCase.Steps.Add(new ActionStep("Open search", "Navigate to search screen"));
        testCase.Steps.Add(new ActionStep("Enter query", "Type a known keyword"));
        testCase.Steps.Add(new ActionStep("Run search", "Press Enter or click Search"));
        testCase.Steps.Add(new AssertStep("Results visible", "At least 1 result shown with the keyword highlighted"));
        return testCase;
      }));
}