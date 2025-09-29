using TestDocCli.Formatters;

namespace TestDocCli.AppCore;

public interface IOutputFormatterFactory
{
  IOutputFormatter Create(string formatArgument);
}
