using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TestResultParser
{
    public static List<TestResult> Parse(string filePath)
    {
        var lines = File.ReadAllLines(filePath).ToList();
        var results = new List<TestResult>();

        for (int i = 0; i < lines.Count; i++)
        {
            var l = lines[i].Trim();
            if (l.Length == 0) continue;
            if (i == 0 && l.StartsWith("Suite,TestName,Status")) continue; // skip header
            var parts = l.Split(',');

            if (parts.Length < 5)
            {
                continue;
            }
            
            var suite = parts[0].Trim();
            var testName = parts[1].Trim();
            var status = parts[2].Trim().ToUpperInvariant();
            results.Add(new TestResult(suite, testName, status));
        }

        return results;
    }
}