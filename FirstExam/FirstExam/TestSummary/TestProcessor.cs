using System.Collections.Generic;

public class TestProcessor
{
    public static TestSummary Process(List<TestResult> results)
    {
        var summary = new TestSummary();
        var seenFail = new HashSet<string>();

        foreach (var r in results)
        {
            summary.Total++;
            var key = r.Suite + "/" + r.TestName;
            
            if (r.Status == "PASS")
            {
                summary.Passed++;
                summary.PassingTests.Add(key);
            }
            else if (r.Status == "FAIL")
            {
                summary.Failed++;

                if (!seenFail.Contains(key))
                {
                    summary.FailingTests.Add(key);
                    seenFail.Add(key);
                }
            }
        }
        return summary;
    }
}