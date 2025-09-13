namespace CliTool.Abstractions;

public abstract class TestStep
{
  public string Title { get; }

  protected TestStep(string title) => Title = title;

  public abstract string Describe();
}