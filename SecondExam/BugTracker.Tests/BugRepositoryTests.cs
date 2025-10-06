using BugTracker.Domain;
using BugTracker.Infrastructure;
using Xunit;

public class BugRepositoryTests
{
    [Fact]
    public void Add_AssignsIdAndStoresBug()
    {
        var repo = new BugRepository();
        var bug = new Bug(0, "Test bug", Severity.Low);

        var saved = repo.Add(bug);

        Assert.Equal(1, saved.Id);
        Assert.Equal("Test bug", saved.Title);
        Assert.Equal(Severity.Low, saved.Severity);
    }

    [Fact]
    public void GetById_ReturnsCorrectBug()
    {
        var repo = new BugRepository();
        var bug = repo.Add(new Bug(0, "Bug to find by ID", Severity.Medium));

        var found = repo.GetById(bug.Id);

        Assert.NotNull(found);
        Assert.Equal("Bug to find by ID", found!.Title);
    }

    [Fact]
    public void Save_UpdatesExistingBug()
    {
        var repo = new BugRepository();
        var bug = repo.Add(new Bug(0, "Bug to fix", Severity.Medium));
        bug.Fix();
        repo.Save(bug);

        var updated = repo.GetById(bug.Id);
        Assert.True(updated!.IsFixed);
    }
}