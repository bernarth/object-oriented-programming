using AutoFixture;
using BugTracker.Domain;

namespace BugTracker.Tests.Domain;

public class BugTests
{

  private readonly Fixture _fixture = new();

  [Fact]
  public void Fix_ChangeFixedToTrue_ShouldChangeIsFixedToTrue()
  {
    int id = 10;
    string title = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    var bug = new Bug(id, title, severity);

    bool result = bug.Fix();

    Assert.True(bug.IsFixed);
  }
  [Fact]
  public void Rename_ChangeBugTitle_ShouldChangeBugTitle()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    string newTitle = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    int id = 10;
    var bug = new Bug(id, title, severity);

    bug.Rename(newTitle);

    Assert.Equal(newTitle, bug.Title);
  }
}