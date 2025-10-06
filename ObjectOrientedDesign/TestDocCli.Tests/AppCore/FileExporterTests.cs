using AutoFixture;
using TestDocCli.AppCore;
using TestDocCli.Errors;

namespace TestDocCli.Tests.AppCore;

public class FileExporterTests
{
  private readonly Fixture _fixture = new();

  [Theory]
  [InlineData("")]
  [InlineData("     ")]
  public void Save_WithoutDirectory_ShouldUseCurrentDirectory(string emptyDirectory)
  {
    string content = _fixture.Create<string>().Substring(0, 12);
    string extension = _fixture.Create<string>().Substring(0, 12);
    string baseNameHint = _fixture.Create<string>().Substring(0, 12);
    string directory = emptyDirectory;
    var fileExporter = new FileExporter();

    string result = fileExporter.Save(content, extension, baseNameHint, directory);

    Assert.StartsWith(Directory.GetCurrentDirectory(), result);
    Assert.Contains($"{baseNameHint}-", result);
    Assert.EndsWith($".{extension}", result);
    Assert.True(File.Exists(result));
    Assert.Equal(content, File.ReadAllText(result));

    // cleanup
    File.Delete(result);
  }

  [Fact]
  public void Save_WithNewDirectory_ShouldUseNewDirectory()
  {
    string content = _fixture.Create<string>().Substring(0, 12);
    string extension = _fixture.Create<string>().Substring(0, 12);
    string baseNameHint = _fixture.Create<string>().Substring(0, 12);
    string directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    var fileExporter = new FileExporter();

    string result = fileExporter.Save(content, extension, baseNameHint, directory);

    Assert.StartsWith($"{directory}", result);
    Assert.Contains($"{baseNameHint}-", result);
    Assert.EndsWith($".{extension}", result);
    Assert.True(Directory.Exists(directory));
    Assert.True(File.Exists(result));
    Assert.Equal(content, File.ReadAllText(result));

    // cleanup
    Directory.Delete(directory, true);
  }

  [Theory]
  [InlineData("invalidChars\\/?<>:*|")]
  [InlineData("C:/Users/New.User/TestDocumentFiles/PathDoesNotExist")]
  public void Save_DirectoryWithInvalidName_ShouldThrowExportException(string invalidDirectory)
  {
    string content = _fixture.Create<string>().Substring(0, 12);
    string extension = _fixture.Create<string>().Substring(0, 12);
    string baseNameHint = _fixture.Create<string>().Substring(0, 12);
    string directory = invalidDirectory;
    var fileExporter = new FileExporter();

    Assert.Throws<ExportException>(() => fileExporter.Save(content, extension, baseNameHint, directory));
  }
  
}