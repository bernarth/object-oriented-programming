// STRATEGY

// Context
// Strategy - Interface
// Concrete Strategies
// Client

using TemplateStrategy;
using TemplateStrategy.Template;

// WITHOUT STRATEGY
static void ExportReport(string format, string reportContent)
{
  if (format == "json")
  {
    Console.WriteLine($"{{ \"report\": \" {reportContent} \" }}");
  }
  else if (format == "csv")
  {
    Console.WriteLine($"""
    report
    {reportContent}
    """);
  }
  else if (format == "xml")
  {
    Console.WriteLine($"<report> {reportContent} </report>");
  }
  else
  {
    Console.WriteLine("Unkown format");
  }
}

ExportReport("xml", "Hello World");

// WITH STRATEGY
Console.WriteLine("Choose export format (json|csv|xml)");

string? format = Console.ReadLine();

IReportExportStrategy strategy = format switch
{
  "json" => new JsonReportExportStrategy(),
  "csv" => new CsvReportExportStrategy(),
  "xml" => new XmlReportExportStrategy(),
  _ => throw new ArgumentException("Unkown format argument")
};

var exporter = new ReportExporterContext(strategy);
exporter.ExportReport("Hello World");

// TEMPLATE METHOD

// Abstract class
// Concretes

// WITH TEMPLATE METHOD

var testCaseResult = new TestCaseResult
{
  Id = "123",
  Name = "Login Process",
  Status = "Passed"
};

Console.WriteLine("Choose format (json|csv)");
string? reportFormat = Console.ReadLine();

ReportExporterBase reportExporter = reportFormat switch
{
  "json" => new JsonReportExporter(),
  "csv" => new CsvReportExporter(),
  _ => throw new ArgumentException("Unkown format argument")
};

reportExporter.Export(testCaseResult);