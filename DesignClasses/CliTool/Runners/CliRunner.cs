using CliTool.Entities;
using CliTool.Interfaces;

namespace CliTool.Runners;

public sealed class CliRunner(IEnumerable<IExporter> exporters, TestCaseBuilder builder)
{
  private readonly IEnumerable<IExporter> _exporters = exporters;
  private readonly TestCaseBuilder _builder = builder;

  public int Run(string[] args)
  {
    var options = CliOptions.Parse(args);

    if (options.ListTemplates)
    {
      PrintTemplates();

      return 0;
    }

    if (string.IsNullOrWhiteSpace(options.TemplateId) ||
      string.IsNullOrWhiteSpace(options.Name))
    {
      PrintHelp();

      return 2;
    }

    var testCase = _builder.BuildFromTemplate(options.TemplateId, options.Name, options.Purpose);

    var suite = new TestSuite(options.Suite ?? "Ad-hoc Suite");
    suite.Cases.Add(testCase);

    var doc = new TestCaseDocument();
    doc.AddSection("Name", testCase.Name);
    doc.AddSection("Purpose", testCase.Purpose);
    doc.AddSection("Steps", string.Join(Environment.NewLine, testCase.Steps.Select((s, i) => $"{i + 1}. {s.Describe()}")));

    foreach (IExporter exporter in SelectExporters(_exporters, options.Exporter))
    {
      exporter.Export(doc);
    }

    return 0;
  }

  private static IEnumerable<IExporter> SelectExporters(IEnumerable<IExporter> all, string[]? names)
  {
    if (names is null || names.Length == 0)
    {
      return all;
    }

    var set = names.Select(n => n.Trim().ToLowerInvariant()).ToHashSet();

    return all.Where(e => set.Contains(e.GetType().Name.ToLowerInvariant()));
  }

  private static void PrintTemplates()
  {
    foreach (var template in DemoTemplates.Library.All())
    {
      Console.WriteLine($" - {template.Id,-12} : {template.Description}");
    }
  }

  private void PrintHelp()
  {
    Console.WriteLine($"""
    Usage: 
      dotnet run -- --template=<id> --name="My Test Case"
    
    Options:
      --list
      --template=<id>
      --name"..."
      --export=A,B
      --out=path.md
    """);
  }
}