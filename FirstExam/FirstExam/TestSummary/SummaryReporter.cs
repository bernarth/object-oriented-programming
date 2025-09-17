using System;
using System.Linq;

public class SummaryReporter
{
    public static void Report(TestSummary summary, Arguments arguments)
    {
        Console.WriteLine("==== Test Summary ====");
        Console.WriteLine("File: " + arguments.FilePath);
        Console.WriteLine("Total: " + summary.Total);
        Console.WriteLine("Passed: " + summary.Passed);
        Console.WriteLine("Failed: " + summary.Failed);
        Console.WriteLine();
        Console.WriteLine("Passing Tests:");
        

        if (summary.PassingTests.Count == 0)
        {
            Console.WriteLine("(none)");
        }
        else
        {
            foreach (var t in summary.PassingTests.OrderBy(x => x))
            {
                Console.WriteLine("- " + t);
            }
        }
       Console.WriteLine();
        Console.WriteLine("Failed Tests:");
        if (summary.FailingTests.Count == 0)
        {
            Console.WriteLine("(none)");
        }
        else
        {
            foreach (var t in summary.FailingTests.OrderBy(x => x))
            {
                Console.WriteLine("- " + t);
            }
        }

        if (arguments.Notify)
        {
            Console.WriteLine();
            Console.WriteLine("NOTIFY => #qa-alerts | failed=" + summary.Failed + " | unique failing tests=" + summary.FailingTests.Count);
        }
    }
}