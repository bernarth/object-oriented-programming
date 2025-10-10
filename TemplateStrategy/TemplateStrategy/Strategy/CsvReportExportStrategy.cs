using System;

namespace TemplateStrategy;

public class CsvReportExportStrategy : IReportExportStrategy
{
  public void Export(string reportContent)
  {
    Console.WriteLine("Exporting report as CSV...");
    Console.WriteLine($"""
    report
    {reportContent}
    """);
  }
}
