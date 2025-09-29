namespace TestDocCli.AppCore;

public sealed class FileExporter : IFileExporter
{
  public string Save(string content, string extension, string baseNameHint, string directory)
  {
    if (string.IsNullOrWhiteSpace(directory))
    {
      directory = Directory.GetCurrentDirectory();
    }

    if (!Directory.Exists(directory))
    {
      Directory.CreateDirectory(directory);
    }

    // TODO: Normalize the baseNameHint so we can use the title of the document as the name
    string baseName = string.IsNullOrWhiteSpace(baseNameHint) ? "testdoc" : baseNameHint;
    string timestamp = DateTimeOffset.Now.ToString("yyyyMMdd-HHmmss");
    string fileName = $"{baseName}-{timestamp}.{extension}";
    string fullPath = Path.Combine(directory, fileName);

    File.WriteAllText(fullPath, content);

    return fullPath;
  }
}
