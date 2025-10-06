public class BugRepositoryTests
{

  private readonly Fixture _fixture = new();

  [Fact]
  public void Add_OneBug_ShouldCreateOneBug()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    string severity = _fixture.Create<string>().Substring(0, 12);
    var bug = new BugRepositoryTests
    {
      Title = title,
      Severity = severity
    };
    var bugRepository = new BugRepository();

    Bug result = bugRepository.Add(bug);

    Assert.Equal(bug.Title, result.Title);
    Assert.Equal(bug.Severity, result.Severity);
    Assert.Equal(1, result.Id);


  }
}