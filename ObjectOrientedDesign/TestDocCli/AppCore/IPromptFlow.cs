using TestDocCli.Model;

namespace TestDocCli.AppCore;

public interface IPromptFlow
{
  TestDocument CollectTestDocument();
}
