using TestDocCli.Errors;

namespace TestDocCli.AppCore;

public sealed class FileExporter : IFileExporter
{
  public string Save(string content, string extension, string baseNameHint, string directory)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(directory))
      {
        directory = Directory.GetCurrentDirectory();
      }

      if (!Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }

      string baseName = NormalizeFileName(baseNameHint);
      string timestamp = DateTimeOffset.Now.ToString("yyyyMMdd-HHmmss");
      string fileName = $"{baseName}-{timestamp}.{extension}";
      string fullPath = Path.Combine(directory, fileName);

      File.WriteAllText(fullPath, content);
    
      return fullPath;
    }
    catch (Exception ioException)
    {
      throw new ExportException("Could not write the file. Check permissions or path", ioException);
    }
  }

  private static string NormalizeFileName(string baseName)
  {
    if (string.IsNullOrWhiteSpace(baseName))
      return "testdoc";

    foreach (var c in Path.GetInvalidFileNameChars())
    {
      baseName = baseName.Replace(c, '-');
    }

    baseName = baseName.Trim();

    return baseName.Length > 100 ? baseName[..100] : baseName;
  }
}
