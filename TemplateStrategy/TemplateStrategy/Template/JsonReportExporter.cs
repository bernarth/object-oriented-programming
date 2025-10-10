namespace TemplateStrategy.Template;

public class JsonReportExporter : ReportExporterBase
{
  protected override void WriteHeader(TestCaseResult testCaseResult)
  {
    Console.WriteLine($"{{ \"Id\": \"{testCaseResult.Id}\",");
  }

  protected override void WriteBody(TestCaseResult testCaseResult)
  {
    Console.WriteLine($"\"Name\": \"{testCaseResult.Name}\",");
  }

  protected override void WriteFooter(TestCaseResult testCaseResult)
  {
    Console.WriteLine($"\"Status\": \"{testCaseResult.Status}\" }}");
  }
}
