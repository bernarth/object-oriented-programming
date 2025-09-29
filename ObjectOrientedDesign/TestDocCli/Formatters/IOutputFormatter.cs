using TestDocCli.Model;

namespace TestDocCli.Formatters;

public interface IOutputFormatter
{
  string FileExtension { get; } // txt, md, html
  string Format(TestDocument document);
}