using TestDocCli.Errors;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TestDocCli.AppCore;

public sealed class FileExporter : IFileExporter
{
  private string NormalizeTile(string title)
  {
    var textInfo = CultureInfo.InvariantCulture.TextInfo;
    title = Regex.Replace(title, @"[^\w]", "_");
    title = textInfo.ToTitleCase(title.ToLowerInvariant());
    return title.Replace(" ", "");
  }
  
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

      // SOLVE: Normalize the baseNameHint so we can use the title of the document as the name
      string baseName = string.IsNullOrWhiteSpace(baseNameHint) ? "testdoc" : NormalizeTile(baseNameHint);
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
}
