using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Formatters;

namespace TestDocCli.Tests.AppCore;

public class OutputFormatterFactoryTests
{
  [Theory]
  [InlineData("", typeof(PlainConsoleFormatter))]
  [InlineData("console", typeof(PlainConsoleFormatter))]
  [InlineData("  console ", typeof(PlainConsoleFormatter))]
  [InlineData("CONsole ", typeof(PlainConsoleFormatter))]
  [InlineData("text", typeof(PlainConsoleFormatter))]
  [InlineData("TEXT", typeof(PlainConsoleFormatter))]
  [InlineData("html", typeof(HtmlFormatter))]
  [InlineData("HTML", typeof(HtmlFormatter))]
  [InlineData("md", typeof(MarkdownFormatter))]
  [InlineData("mD", typeof(MarkdownFormatter))]
  [InlineData("markdown", typeof(MarkdownFormatter))]
  public void Create_KnownHtmlFormat_ReturnsHtmlFormatter(string input, Type expectedType)
  {
    var factory = new OutputFormatterFactory();

    IOutputFormatter formatter = factory.Create(input);

    Assert.IsType(expectedType, formatter);
  }

  [Theory]
  [InlineData("_text")]
  [InlineData("txt")]
  [InlineData("pdf")]
  [InlineData("cnsolee")]
  [InlineData("m")]
  [InlineData("123&%$")]
  public void Create_UnknownFormat_ThrowsConfigurationException(string unknownFormat)
  {
    var factory = new OutputFormatterFactory();

    Assert.Throws<ConfigurationException>(() => factory.Create(unknownFormat));
  }
  // DONE: Add more tests.  
}
