namespace TestDocCli.InputOutput;

public interface IInputReader
{
  string ReadRequired(string label);
  string ReadLine(string prompt);
}