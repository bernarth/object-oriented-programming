using System.Text.RegularExpressions;
using System.Globalization;
namespace TestDocCli.AppCore;

public sealed class FileExporter : IFileExporter
{
  private static string NormalizeTitle(string title)
  {
    TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
    title = Regex.Replace(title, "[^a-zA-Z0-9]", " ");
    return textInfo.ToTitleCase(title).Replace(" ", "");
  }
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

    // SOLVED: Normalize the baseNameHint so we can use the title of the document as the name
    string baseName = string.IsNullOrWhiteSpace(baseNameHint) ? "testdoc" : NormalizeTitle(baseNameHint);
    string timestamp = DateTimeOffset.Now.ToString("yyyyMMdd-HHmmss");
    string fileName = $"{baseName}-{timestamp}.{extension}";
    string fullPath = Path.Combine(directory, fileName);

    File.WriteAllText(fullPath, content);

    return fullPath;
  }
}
