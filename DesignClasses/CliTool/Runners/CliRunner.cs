using CliTool.Entities;
using CliTool.Interfaces;

namespace CliTool.Runners;

public sealed class CliRunner(IEnumerable<IExporter> exporters)
{
  private readonly IEnumerable<IExporter> _exporters = exporters;

  public int Run()
  {
    return 0;
  }
}