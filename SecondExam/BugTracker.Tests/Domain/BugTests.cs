using BugTracker.Domain;
using BugTracker.Infrastructure;
using Xunit;
using System;
using AutoFixture;

namespace BugTracker.Tests;

public class BugTests
{
    private readonly Fixture _fixture = new();

    public BugTests()
    {
        _fixture.Register<Bug>(() => new Bug(
            _fixture.Create<int>(),
            _fixture.Create<string>().Substring(0, 10),
            _fixture.Create<Severity>()
        ));
    }

    [Fact]
    public void Bug_Constructor_ValidParameters_CreatesBugWithCorrectState()
    {
        int id = _fixture.Create<int>();
        string title = _fixture.Create<string>().Substring(0, 10);
        Severity severity = _fixture.Create<Severity>();

        var bug = new Bug(id, title, severity);

        Assert.Equal(id, bug.Id);
        Assert.Equal(title, bug.Title);
        Assert.Equal(severity, bug.Severity);
        Assert.False(bug.IsFixed);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Bug_Constructor_InvalidTitle_ThrowsArgumentException(string invalidTitle)
    {
        int id = _fixture.Create<int>();
        Severity severity = _fixture.Create<Severity>();
        Assert.Throws<ArgumentException>(() => new Bug(id, invalidTitle, severity));
    }

    [Fact]
    public void Fix_OpenBug_SetsIsFixedToTrue()
    {
        var bug = _fixture.Create<Bug>();
        bug.Fix();
        Assert.True(bug.IsFixed);
    }

    [Fact]
    public void Rename_Bug_ChangesTitleCorrectly()
    {
        var bug = _fixture.Create<Bug>();
        string newTitle = _fixture.Create<string>().Substring(0, 15);
        bug.Rename(newTitle);
        Assert.Equal(newTitle, bug.Title);
    }
}