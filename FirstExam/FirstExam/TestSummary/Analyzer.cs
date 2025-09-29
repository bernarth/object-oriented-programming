using TestSummary.Entities;

namespace TestSummary;

public class Analyzer
{
  public Summary Analyze(IEnumerable<TestResult> results)
  {
    var totalTests = results.Count();
    var passedTests = results.Count(r => r.Status == Status.Pass);
    var failedTests = results.Count(r => r.Status == Status.Fail);

    return new Summary
    {
      TotalTests = totalTests,
      PassedTests = passedTests,
      FailedTests = failedTests
    };
  }

  public IEnumerable<string> UniqueFailingTests(IEnumerable<TestResult> results)
  {
    var seenFail = new HashSet<string>();

    foreach (var r in results.Where(r => r.Status == Status.Fail))
    {
      var key = r.Suite + "/" + r.TestName;
      seenFail.Add(key);
    }

    return seenFail;
  }
}