using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSummary
{
    public class ReportWriter
    {
        public void Write(TestSummary testSummary, string filePath)
        {
            Console.WriteLine("==== Test Summary ====");
            Console.WriteLine("File: " + filePath);
            Console.WriteLine("Total: " + testSummary.Total);
            Console.WriteLine("Passed: " + testSummary.Passed);
            Console.WriteLine("Failed: " + testSummary.Failed);
            Console.WriteLine();
            Console.WriteLine("Failing Tests:");

            if (testSummary.FailingTests.Count == 0)
            {
                Console.WriteLine("(none)");
            }
            else
            {
                foreach (var t in testSummary.FailingTests.OrderBy(x => x))
                {
                    Console.WriteLine("- " + t);
                }
            }
        }
    }
}
