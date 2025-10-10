namespace TemplateStrategy;

public class JsonReportExportStrategy : IReportExportStrategy
{
  public void Export(string reportContent)
  {
    Console.WriteLine("Exporting report as JSON...");
    Console.WriteLine($"{{ \"report\": \" {reportContent} \" }}");
  }
}