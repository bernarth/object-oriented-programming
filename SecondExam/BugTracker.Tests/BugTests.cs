using BugTracker.Domain;
using Xunit;

public class BugTests
{
    [Fact]
    public void Bug_IsCreatedWithCorrectProperties()
    {
        var bug = new Bug(1, "Sample bug", Severity.High);

        Assert.Equal(1, bug.Id);
        Assert.Equal("Sample bug", bug.Title);
        Assert.Equal(Severity.High, bug.Severity);
        Assert.False(bug.IsFixed);
    }

    [Fact]
    public void Fix_SetsIsFixedToTrue()
    {
        var bug = new Bug(2, "Another bug", Severity.Low);
        bug.Fix();

        Assert.True(bug.IsFixed);
    }

    [Fact]
    public void Rename_ChangesTitle()
    {
        var bug = new Bug(3, "Old title", Severity.Medium);
        bug.Rename("New title");

        Assert.Equal("New title", bug.Title);
    }
}