using AutoFixture;
using BugTracker.Infrastructure;
using BugTracker.Domain;

namespace BugTracker.Tests.Infrastructure;

public class BugRepositoryTests
{

  private readonly Fixture _fixture = new();

  [Fact]
  public void Add_OneBug_ShouldCreateOneBug()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    int id = 10;
    var bug = new Bug(id, title, severity);
    var bugRepository = new BugRepository();

    Bug result = bugRepository.Add(bug);

    Assert.Equal(bug.Title, result.Title);
    Assert.Equal(bug.Severity, result.Severity);
    Assert.Equal(1, result.Id);
  }
  [Fact]
  public void GetById_GetFirstId_ShouldReturnFirst()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    int id = 10;
    var bug = new Bug(id, title, severity);
    var bugRepository = new BugRepository();
    bugRepository.Add(bug);

    Bug? result = bugRepository.GetById(1);

    Assert.Equal(bug.Title, result?.Title);
    Assert.Equal(bug.Severity, result?.Severity);
    Assert.Equal(1, result?.Id);
  }

  [Fact]
  public void GetById_NonExistentId_ShouldReturnNull()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    int id = 10;
    var bug = new Bug(id, title, severity);
    var bugRepository = new BugRepository();
    bugRepository.Add(bug);

    Bug? result = bugRepository.GetById(2);

    Assert.Null(result);
  }
  
  [Fact]
  public void GetAll_ShouldReturnList()
  {
    string title = _fixture.Create<string>().Substring(0, 12);
    Severity severity = Severity.Medium;
    int id = 10;
    var bug = new Bug(id,title,severity);
    var bugRepository = new BugRepository();
    bugRepository.Add(bug);

    List<Bug> result = bugRepository.GetAll();

    Assert.IsType<List<Bug>>(result);
  }
}