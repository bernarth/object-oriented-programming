using BugTracker.Domain;
using BugTracker.Infrastructure;
using Xunit;
using System.Linq;
using AutoFixture;

namespace BugTracker.Tests;

public class BugRepositoryTests
{
    private readonly Fixture _fixture = new();

    public BugRepositoryTests()
    {
        _fixture.Register<Bug>(() => new Bug(
            0,
            _fixture.Create<string>().Substring(0, 10),
            _fixture.Create<Severity>()
        ));
    }

    [Fact]
    public void Add_NewBug_AssignsUniqueIdAndAddsToCollection()
    {
        var repository = new BugRepository();
        var bug = _fixture.Create<Bug>();

        var addedBug = repository.Add(bug);

        Assert.NotEqual(0, addedBug.Id);
        Assert.Equal(1, addedBug.Id);
        Assert.Contains(addedBug, repository.GetAll());
    }

    [Fact]
    public void GetById_ExistingBug_ReturnsCorrectBug()
    {
        var repository = new BugRepository();
        var bug1 = repository.Add(_fixture.Create<Bug>());
        repository.Add(_fixture.Create<Bug>());

        var foundBug = repository.GetById(bug1.Id);

        Assert.NotNull(foundBug);
        Assert.Equal(bug1.Id, foundBug.Id);
    }

    [Fact]
    public void GetById_NonExistingBug_ReturnsNull()
    {
        var repository = new BugRepository();
        repository.Add(_fixture.Create<Bug>());

        var foundBug = repository.GetById(_fixture.Create<int>() + 100);

        Assert.Null(foundBug);
    }

    [Fact]
    public void Update_ExistingBug_ModifiesBugInRepository()
    {
        var repository = new BugRepository();
        var originalBug = repository.Add(_fixture.Create<Bug>());
        
        var updatedTitle = _fixture.Create<string>().Substring(0, 15);
        var updatedBug = new Bug(originalBug.Id, updatedTitle, Severity.High);
        updatedBug.Fix();

        repository.Update(updatedBug);
        var retrievedBug = repository.GetById(originalBug.Id);

        Assert.NotNull(retrievedBug);
        Assert.Equal(updatedTitle, retrievedBug.Title);
        Assert.Equal(Severity.High, retrievedBug.Severity);
        Assert.True(retrievedBug.IsFixed);
    }
}