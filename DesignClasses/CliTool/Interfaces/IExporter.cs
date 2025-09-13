using CliTool.Entities;

namespace CliTool.Interfaces;

public interface IExporter
{
  void Export(TestCaseDocument document);
}