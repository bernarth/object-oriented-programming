using TestDocCli.Formatters;
using TestDocCli.Model;

namespace TestDocCli.Tests.Formatters;

public class MarkDownFormatterTests
{
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
}
