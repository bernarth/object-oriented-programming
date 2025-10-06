using BugTracker.Domain;
using BugTracker.Infrastructure;

namespace BugTracker.Application;

public class BugTrackerService
{
  private readonly BugRepository _repository;

  public BugTrackerService(BugRepository repository)
  {
    _repository = repository;
  }

  public Bug ReportNew(string title, Severity severity)
  {
    Bug transient = new(0, title, severity);
    Bug saved = _repository.Add(transient);
    return saved;
  }

  public bool FixById(int id)
  {
    Bug? bug = _repository.GetById(id);
    if (bug == null)
    {
      return false;
    }

    bug.Fix();
    _repository.Save(bug);
    return true;
  }

  public List<Bug> GetAll()
  {
    return _repository.GetAll();
  }

  public List<Bug> GetOpenBugs()
  {
    List<Bug> result = [];
    List<Bug> all = _repository.GetAll();

    foreach (Bug bug in all)
    {
      if (bug.IsFixed == false)
      {
        result.Add(bug);
      }
    }

    return result;
  }

  public int CountOpenBySeverity(Severity severity)
  {
    int count = 0;
    List<Bug> all = _repository.GetAll();

    foreach (Bug bug in all)
    {
      if (bug.IsFixed == false && bug.Severity == severity)
      {
        count++;
      }
    }

    return count;
  }
}
