using AutoFixture;
using TestDocCli.Formatters;
using TestDocCli.Model;

public class PlainConsoleFormatterTests
{
  private readonly Fixture _fixture = new();

  [Fact]
  public void Format_PlainConsoleFormatter_ShouldReturnTestData()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    string description = _fixture.Create<string>().Substring(0, 12);
    List<string> steps = _fixture.CreateMany<string>(3)
    .Select(s => s.Substring(0, Math.Min(10, s.Length))).ToList();
    string expected = _fixture.Create<string>().Substring(0, 12);
    string actual = _fixture.Create<string>().Substring(0, 12);
    var document = new TestDocument
    {
      Title = title,
      Description = description,
      Steps = steps,
      Expected = expected,
      Actual = actual,
      CreatedAt = DateTimeOffset.Now
    };
    var formatter = new PlainConsoleFormatter();

    string result = formatter.Format(document);

    Assert.StartsWith("============ TEST DOCUMENT ============", result);
    Assert.Contains($"Title: {document.Title}", result);
    Assert.Contains($"Description: {document.Description}", result);
    Assert.Contains("Steps:", result);
    Assert.Contains($"1. {steps.First()}", result);
    Assert.Contains($"Expected: {document.Expected}", result);
    Assert.Contains($"Actual: {document.Actual}", result);
    Assert.EndsWith(" ============\r\n", result);
  }
}