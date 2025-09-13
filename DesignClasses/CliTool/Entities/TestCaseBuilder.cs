namespace CliTool.Entities;

public static class TestCaseBuilder
{
  public static TestCase BuildFromTemplate(TemplateLibrary library, string templateId, string name, string? purposeOverride = null)
  {
    if (!library.TryGet(templateId, out var template) || template is null)
    {
      throw new InvalidOperationException($"Unknown template {templateId}. List the templates");
    }

    var testCase = template.Factory(name);

    if (!string.IsNullOrWhiteSpace(purposeOverride))
    {
      return new TestCase(testCase.Name, purposeOverride, testCase.Steps);
    }

    return testCase;
  }
}