namespace CliTool.Entities;

public class TestCaseBuilder(TemplateLibrary library)
{
  private readonly TemplateLibrary _library = library;

  public TestCase BuildFromTemplate(string templateId, string name, string? purposeOverride = null)
  {
    if (!_library.TryGet(templateId, out var template) || template is null)
    {
      throw new InvalidOperationException($"Unknown template {templateId}. Use --list to list the templates");
    }

    var testCase = template.Factory(name);

    if (!string.IsNullOrWhiteSpace(purposeOverride))
    {
      return new TestCase(testCase.Name, purposeOverride, testCase.Steps);
    }

    return testCase;
  }
}