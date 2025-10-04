using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Formatters;

namespace TestDocCli.Tests.AppCore;

public class OutputFormatterFactoryTests
{
  [Theory]
  [InlineData("   ", typeof(PlainConsoleFormatter))] // spaces will be trimmed
  [InlineData("", typeof(PlainConsoleFormatter))]
  [InlineData("console", typeof(PlainConsoleFormatter))]
  [InlineData("CONSOLE", typeof(PlainConsoleFormatter))]
  [InlineData("ConSoLe", typeof(PlainConsoleFormatter))]
  [InlineData("text", typeof(PlainConsoleFormatter))]
  [InlineData("TEXT", typeof(PlainConsoleFormatter))]
  [InlineData("TeXt", typeof(PlainConsoleFormatter))]
  [InlineData("html", typeof(HtmlFormatter))]
  [InlineData("HTML", typeof(HtmlFormatter))]
  [InlineData("HtMl", typeof(HtmlFormatter))]
  [InlineData("md", typeof(MarkdownFormatter))]
  [InlineData("MD", typeof(MarkdownFormatter))]
  [InlineData("Md", typeof(MarkdownFormatter))]
  [InlineData("markdown", typeof(MarkdownFormatter))]
  [InlineData("MARKDOWN", typeof(MarkdownFormatter))]
  [InlineData("MaRkDoWn", typeof(MarkdownFormatter))]
  public void Create_KnownFormat_ReturnsKnownFormatter(string input, Type expectedType)
  {
    var factory = new OutputFormatterFactory();

    IOutputFormatter formatter = factory.Create(input);

    Assert.IsType(expectedType, formatter);
  }

  [Theory]
  [InlineData("pdf")]
  [InlineData("12345")]
  [InlineData("*¨?¡[}]")]
  public void Create_UnknownFormat_ThrowsConfigurationException(string invalidInput)
  {
    var factory = new OutputFormatterFactory();

    Assert.Throws<ConfigurationException>(() => factory.Create(invalidInput));
  }

  // SOLVED: Add more tests. More test cases were added for valid and invalid input with InlineData
}
