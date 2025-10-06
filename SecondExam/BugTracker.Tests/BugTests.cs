using BugTracker.Domain;
using Xunit;

namespace BugTracker.Tests;

public class BugTests
{
    [Fact]
    public void CanCreateBug()
    {
        var bug = new Bug(1, "Sample bug", Severity.Low);
        Assert.Equal(1, bug.Id);
        Assert.Equal("Sample bug", bug.Title);
        Assert.Equal(Severity.Low, bug.Severity);
        Assert.False(bug.IsFixed);
    }

    [Fact]
    public void CanFixBug()
    {
        var bug = new Bug(1, "Sample bug", Severity.Low);
        bug.Fix();
        Assert.True(bug.IsFixed);
    }

    [Fact]
    public void CanRenameBug()
    {
        var bug = new Bug(1, "Old Title", Severity.Medium);
        bug.Rename("New Title");
        Assert.Equal("New Title", bug.Title);
    }
}