using BugTracker.Domain;
using BugTracker.Presentation;
using Xunit;
 
namespace BugTracker.Tests;
 
public class ConsoleReportTests
{
    [Fact]
    public void FormatIncludesAllBugData()
    {
        var bug = new Bug(1, "Test Bug", Severity.Medium);
        var output = ConsoleReport.Format(bug);
        Assert.Contains("[1]", output);
        Assert.Contains("Test Bug", output);
        Assert.Contains("Medium", output);
        Assert.Contains("Open", output);
    }
 
    [Fact]
    public void FormatShowsFixedStatus()
    {
        var bug = new Bug(1, "Test Bug", Severity.Low);
        bug.Fix();
        var output = ConsoleReport.Format(bug);
        Assert.Contains("Fixed", output);
    }
 
    [Fact]
    public void PrintDoesNotThrow()
    {
        var bugs = new[] { new Bug(1, "Bug1", Severity.High), new Bug(2, "Bug2", Severity.Low) };
        var exception = Record.Exception(() => ConsoleReport.Print(bugs));
        Assert.Null(exception);
    }
}