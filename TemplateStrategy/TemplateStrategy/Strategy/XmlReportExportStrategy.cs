using System;

namespace TemplateStrategy;

public class XmlReportExportStrategy : IReportExportStrategy
{
  public void Export(string reportContent)
  {
    Console.WriteLine("Exporting report as XML...");
    Console.WriteLine($"<report> {reportContent} </report>");
  }
}
