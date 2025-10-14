using BugTracker.Domain;
using BugTracker.Infrastructure;

namespace BugTracker.Tests;

public class BugRepositoryTests
{
    [Fact]
    public void AddBugs()
    {
        var repository = new BugRepository();
        var bug1 = new Bug(0, "Login Fail", Severity.High);
        var bug2 = new Bug(0, "API returns 404", Severity.Low);

        var saved1 = repository.Add(bug1);
        var saved2 = repository.Add(bug2);

        Assert.Equal(1, saved1.Id);
        Assert.Equal("Login Fail", saved1.Title);
        Assert.Equal(Severity.High, saved1.Severity);

        Assert.Equal(2, saved2.Id);
        Assert.Equal("API returns 404", saved2.Title);
        Assert.Equal(Severity.Low, saved2.Severity);

        var allBugs = repository.GetAll();
        Assert.Equal(2, allBugs.Count);
    }

    [Fact]
    public void GetByIdBug()
    {
        var repository = new BugRepository();
        var saved = repository.Add( new Bug(0, "API 400", Severity.High));

        var existingBug = repository.GetById(saved.Id);
        var unexistingBug = repository.GetById(500);

        Assert.NotNull(existingBug);
        Assert.Null(unexistingBug);
    }

    [Fact]
    public void SaveUpdatesInMemory()
    {
        var repository = new BugRepository();
        var saved = repository.Add(new Bug(0, "Old title", Severity.Low));

        saved.Rename("New title");
        saved.Severity = Severity.High;
        saved.Fix();
        repository.Save(saved);

        var actualSaved = repository.GetById(saved.Id);

        Assert.NotNull(actualSaved);
        Assert.Equal("New title", actualSaved.Title);
        Assert.Equal(Severity.High, actualSaved.Severity);
        Assert.True(actualSaved.IsFixed);
    }

    [Fact]
    public void GetAllBugs()
    {
        var repository = new BugRepository();
        repository.Add(new Bug(0, "1", Severity.Low));
        repository.Add(new Bug(0, "2", Severity.Medium));
        repository.Add(new Bug(0, "3", Severity.High));

        var allBugs = repository.GetAll();
        Assert.Contains(allBugs, b => b.Title == "1");
        Assert.Contains(allBugs, b => b.Title == "2");
        Assert.Contains(allBugs, b => b.Title == "3");
    }
}
