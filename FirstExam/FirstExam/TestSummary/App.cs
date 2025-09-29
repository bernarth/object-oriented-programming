using TestSummary.Entities;

namespace TestSummary;

public class App(CsvReader reader, Analyzer analyzer, Reporter reporter)
{
  private readonly CsvReader _reader = reader;
  private readonly Analyzer _analyzer = analyzer;
  private readonly Reporter _reporter = reporter;

  public void Run(string filePath, bool notify)
  {
    List<TestResult> results = [.. _reader.ReadAll(filePath)];
    Summary summary = _analyzer.Analyze(results);
    List<string> failingTests = [.. _analyzer.UniqueFailingTests(results)];
    _reporter.Report(summary, failingTests, filePath, notify);
  }
}