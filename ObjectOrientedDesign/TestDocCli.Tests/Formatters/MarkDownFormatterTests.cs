using AutoFixture;
using TestDocCli.Formatters;
using TestDocCli.Model;

namespace TestDocCli.Tests.Formatters;

public class MarkDownFormatterTests
{
  private readonly Fixture _fixture = new();

  [Fact]
  public void Format_MarkdownFormatter_ShouldReturnTestData()
  {
    var document = new TestDocument
    {
      Title = "Checkout",
      Description = "User can checkout",
      Steps =
      [
        "Add to cart",
        "Pay"
      ],
      Expected = "Order completed",
      Actual = "Order un-completed",
      CreatedAt = DateTimeOffset.Parse("2025-01-01T00:00:00Z")
    };
    var formatter = new MarkdownFormatter();

    string result = formatter.Format(document);

    Assert.StartsWith("# Checkout", result);
    Assert.Contains("## Steps", result);
    Assert.Contains("1. Add to cart", result);
  }

  [Fact]
  public void Format_HtmlFormatter_ShouldReturnTestData()
  {
    var document = new TestDocument
    {
      Title = "Checkout",
      Description = "User can checkout",
      Steps =
      [
        "Add to cart",
        "Pay"
      ],
      Expected = "Order completed",
      Actual = "Order un-completed",
      CreatedAt = DateTimeOffset.Parse("2025-01-01T00:00:00Z")
    };
    var formatter = new HtmlFormatter();

    string result = formatter.Format(document);

    Assert.StartsWith("<!doctype html>", result);
    Assert.Contains("<HTML", result, StringComparison.OrdinalIgnoreCase);
    Assert.Contains("<ol>", result, StringComparison.OrdinalIgnoreCase);
    Assert.Contains("Checkout", result);
  }

  [Fact]
  public void Format_MarkdownFormatterWithAutoFixture_ShouldReturnTestData()
  {
    //var fixture = new Fixture();

    string title = _fixture.Create<string>().Substring(0, 12);
    string description = _fixture.Create<string>().Substring(0, 24);
    List<string> steps = [.. _fixture.CreateMany<string>(3).Select(s => s.Substring(0, Math.Min(10, s.Length)))];
    string expected = _fixture.Create<string>().Substring(0, 12);
    string actual = expected;
    var document = new TestDocument
    {
      Title = title,
      Description = description,
      Steps = steps,
      Expected = expected,
      Actual = actual,
      CreatedAt = DateTimeOffset.Now
    };
    var formatter = new MarkdownFormatter();

    string result = formatter.Format(document);

    Assert.StartsWith($"# {title}", result);
    Assert.Contains("## Steps", result);
    Assert.Contains($"1. {steps.First()}", result);
  }

  [Fact]
  public void Format_HtmlFormatterWithAutoFixture_ShouldReturnTestData()
  {
    //var fixture = new Fixture();
    var document = _fixture.Create<TestDocument>();
    var formatter = new HtmlFormatter();

    string result = formatter.Format(document);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
    Assert.StartsWith("<!doctype html>", result, StringComparison.OrdinalIgnoreCase);
    Assert.EndsWith("</html>\r\n", result, StringComparison.OrdinalIgnoreCase);
    Assert.Contains(document.Title, result);
    Assert.Contains(document.Description, result);
    Assert.Contains(document.Actual, result);
    Assert.Contains(document.Expected, result);
  }
}
