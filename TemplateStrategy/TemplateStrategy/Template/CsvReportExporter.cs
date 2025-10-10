namespace TemplateStrategy.Template;

public class CsvReportExporter : ReportExporterBase
{
  protected override void WriteHeader(TestCaseResult testCaseResult)
  {
    Console.WriteLine("Id,Name,Status");
  }

  protected override void WriteBody(TestCaseResult testCaseResult)
  {
    Console.WriteLine($"{testCaseResult.Id},{testCaseResult.Name},{testCaseResult.Status}");
  }

  protected override void WriteFooter(TestCaseResult testCaseResult)
  {
    Console.WriteLine(string.Empty);
  }

  protected override void Save(TestCaseResult testCaseResult)
  {
    Console.WriteLine("Saved in AWS S3 bucket");
  }
}