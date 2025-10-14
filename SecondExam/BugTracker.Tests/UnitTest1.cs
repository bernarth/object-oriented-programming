using BugTracker.Domain;
using BugTracker.Application;
using BugTracker.Infrastructure;
using Xunit;

namespace BugTracker.Tests;

public class BugRepositoryTests
{
    [Fact]
    public void Add_SingleBug_ShouldAssignIdAndStore()
    {
        //Arrange
        var repo = new BugRepository();
        var bug = new Bug(0, "Test bug", Severity.Low);

        //Act
        var saved = repo.Add(bug);

        //Assert
        Assert.Equal(1, saved.Id);
        Assert.Equal("Test bug", saved.Title);
        Assert.Single(repo.GetAll());
    }

    [Fact]
    public void GetById_SingleBug_ShouldReturn()
    {
        //Arrange
        var repo = new BugRepository();
        var bug = new Bug(0, "Test bug", Severity.Low);

        //Act
        var addBug = repo.Add(bug);
        var bugById = repo.GetById(addBug.Id);

        //Assert
        Assert.NotNull(bugById);
        Assert.Equal(addBug.Id, bugById.Id);
    }

    [Fact]
    public void Save_UpdateBugTitle_ShouldSaveChanges()
    {
        //Arrange
        var repo = new BugRepository();
        var bug = new Bug(0, "Test bug", Severity.Low);
        var saved = repo.Add(bug);

        //Act
        saved.Rename("Updated Title");
        repo.Save(saved);

        //Assert
        var updated = repo.GetById(saved.Id);
        Assert.NotNull(updated);
        Assert.Equal("Updated Title", updated.Title);
    }

    [Fact]
    public void Save_UpdateFixBug_ShouldSaveChanges()
    {
        //Arrange
        var repo = new BugRepository();
        var bug = new Bug(0, "Test bug", Severity.Low);
        var saved = repo.Add(bug);

        //Act
        saved.Fix();
        repo.Save(saved);

        //Assert
        var updated = repo.GetById(saved.Id);
        Assert.NotNull(updated);
        Assert.True(updated.IsFixed);
    }

    [Fact]
    public void GetAll_MultipleBugs_ShouldReturnAll()
    {
        //Arrange
        var repo = new BugRepository();
        var bug1 = new Bug(0, "Bug 1", Severity.Low);
        var bug2 = new Bug(0, "Bug 2", Severity.Medium);
        var bug3 = new Bug(0, "Bug 3", Severity.High);

        //Act
        var addedBug1 = repo.Add(bug1);
        var addedBug2 = repo.Add(bug2);
        var addedBug3 = repo.Add(bug3);
        var allBugs = repo.GetAll();

        //Assert
        Assert.Equal(3, allBugs.Count);
        Assert.Contains(addedBug1, allBugs);
        Assert.Contains(addedBug2, allBugs);
        Assert.Contains(addedBug3, allBugs);
    }
}
