using BugTracker.Domain;
using BugTracker.Application;
using BugTracker.Infrastructure;
using Xunit;
using System.IO;
 
namespace BugTracker.Tests;
 
public class BugTrackerServiceTests
{
    private const string TestFile = "service_testbugs.json";
 
    private void CleanTestFile() => File.Delete(TestFile);
 
    [Fact]
    public void CanReportNewBug()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var service = new BugTrackerService(repo);
        var bug = service.ReportNew("New Bug", Severity.High);
        Assert.Equal("New Bug", bug.Title);
    }
 
    [Fact]
    public void CanFixBugById()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var service = new BugTrackerService(repo);
        var bug = service.ReportNew("Bug to Fix", Severity.Low);
        var result = service.FixById(bug.Id);
        Assert.True(result);
        Assert.True(service.GetAll().First(b => b.Id == bug.Id).IsFixed);
    }
 
    [Fact]
    public void CanCountOpenBugsBySeverity()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var service = new BugTrackerService(repo);
        service.ReportNew("Bug1", Severity.High);
        service.ReportNew("Bug2", Severity.High);
        var count = service.CountOpenBySeverity(Severity.High);
        Assert.Equal(2, count);
    }
}