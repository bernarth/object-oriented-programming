using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSummary
{
    public class TestSummary
    {
        public int Total { get; }

        public int Passed { get; }

        public int Failed { get; }

        public List<string> FailingTests { get; }

        public TestSummary(int total, int passed, int failed, List<string> failingTests) 
        {
            Total = total;
            Passed = passed;
            Failed = failed;
            FailingTests = failingTests;
        }

        public TestSummary Calculate(List<string[]> rows)
        {
            int total = 0;
            int passed = 0;
            int failed = 0;
            var failingTests = new List<string>();
            var seenFail = new HashSet<string>();

            foreach (var r in rows)
            {
                total++;
                var suite = r[0].Trim();
                var test = r[1].Trim();
                var status = r[2].Trim().ToUpperInvariant();
                // duration (r[3]) and timestamp (r[4]) ignored in this tiny version

                if (status == "PASS") passed++;
                else if (status == "FAIL")
                {
                    failed++;
                    var key = suite + "/" + test;

                    if (!seenFail.Contains(key))
                    {
                        failingTests.Add(key);
                        seenFail.Add(key);
                    }
                }
            }

            return new TestSummary(total, passed, failed, failingTests);
        }
    }
}
