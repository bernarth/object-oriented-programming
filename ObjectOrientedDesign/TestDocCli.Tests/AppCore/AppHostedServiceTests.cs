using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Formatters;
using TestDocCli.InputOutput;
using TestDocCli.Model;

namespace TestDocCli.Tests.AppCore;

public class AppHostedServiceTests
{
  [Fact]
  public async Task StartAsync_HappyPath_WritesAndSaves()
  {
    string format = "md";
    var fixture = new Fixture();
    IOptions<AppSettings> options = Options.Create(new AppSettings { Format = format, OutputDirectory = Path.GetTempPath() });

    var console = new Mock<IConsole>();
    console.Setup(c => c.WriteLine(It.IsAny<string>())).Verifiable();

    var prompt = new Mock<IPromptFlow>();
    prompt.Setup(p => p.CollectTestDocument()).Returns(fixture.Create<TestDocument>());

    var formatter = new Mock<IOutputFormatter>();
    formatter.SetupGet(f => f.FileExtension).Returns(format);
    formatter.Setup(f => f.Format(It.IsAny<TestDocument>())).Returns("content");

    var factory = new Mock<IOutputFormatterFactory>();
    factory.Setup(f => f.Create(format)).Returns(formatter.Object);

    string tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.{format}");
    var exporter = new Mock<IFileExporter>();
    exporter.Setup(e => e.Save("content", format, string.Empty, It.IsAny<string>())).Returns(tempFile);

    var errors = new Mock<IErrorReporter>();

    var service = new AppHostedService(
      options,
      console.Object,
      prompt.Object,
      factory.Object,
      exporter.Object,
      errors.Object
    );

    await service.StartAsync(CancellationToken.None);

    console.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
    exporter.VerifyAll();
    factory.VerifyAll();
    formatter.VerifyAll();
    prompt.VerifyAll();
  }
}
