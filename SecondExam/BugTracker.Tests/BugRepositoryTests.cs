using BugTracker.Domain;
using BugTracker.Infrastructure;
using Xunit;
using System.IO;
 
namespace BugTracker.Tests;
 
public class BugRepositoryTests
{
    private const string TestFile = "testbugs.json";
 
    private void CleanTestFile() => File.Delete(TestFile);
 
    [Fact]
    public void CanAddBug()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var bug = repo.Add(new Bug(0, "Repo Bug", Severity.High));
        Assert.Equal(1, bug.Id);
        Assert.Equal("Repo Bug", bug.Title);
    }
 
    [Fact]
    public void CanRetrieveBugById()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var bug = repo.Add(new Bug(0, "Repo Bug", Severity.Low));
        var fetched = repo.GetById(bug.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Repo Bug", fetched!.Title);
    }
 
    [Fact]
    public void SaveUpdatesBug()
    {
        CleanTestFile();
        var repo = new BugRepository(TestFile);
        var bug = repo.Add(new Bug(0, "Old Title", Severity.Medium));
        bug.Rename("New Title");
        repo.Save(bug);
        var fetched = repo.GetById(bug.Id);
        Assert.Equal("New Title", fetched!.Title);
    }
}