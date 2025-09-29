namespace TestDocCli.AppCore;

public interface IFileExporter
{
  string Save(string content, string extension, string baseNameHint, string directory);
}
