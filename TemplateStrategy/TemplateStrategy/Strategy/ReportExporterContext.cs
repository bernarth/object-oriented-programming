namespace TemplateStrategy;

public class ReportExporterContext(IReportExportStrategy exportStrategy)
{
  private readonly IReportExportStrategy _exportStrategy = exportStrategy ?? throw new ArgumentNullException(nameof(exportStrategy));

  public void ExportReport(string reportContent)
  {
    // here we have the logic to accomplish the strategy
    _exportStrategy.Export(reportContent);
  }
}
