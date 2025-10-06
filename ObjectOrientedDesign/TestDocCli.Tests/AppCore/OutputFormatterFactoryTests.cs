using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Formatters;

namespace TestDocCli.Tests.AppCore;

public class OutputFormatterFactoryTests
{
  [Theory]
  [InlineData("", typeof(PlainConsoleFormatter))]
  [InlineData("console", typeof(PlainConsoleFormatter))]
  [InlineData("text", typeof(PlainConsoleFormatter))]
  [InlineData("html", typeof(HtmlFormatter))]
  [InlineData("md", typeof(MarkdownFormatter))]
  [InlineData("markdown", typeof(MarkdownFormatter))]
  public void Create_KnownHtmlFormat_ReturnsHtmlFormatter(string input, Type expectedType)
  {
    var factory = new OutputFormatterFactory();

    IOutputFormatter formatter = factory.Create(input);

    Assert.IsType(expectedType, formatter);
  }

  [Fact]
  public void Create_UnknownFormat_ThrowsConfigurationException()
  {
    var factory = new OutputFormatterFactory();

    Assert.Throws<ConfigurationException>(() => factory.Create("pdf"));
  }

  // TODO: Add more tests
}
