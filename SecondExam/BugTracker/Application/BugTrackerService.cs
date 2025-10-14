using BugTracker.Domain;
using BugTracker.Infrastructure;

namespace BugTracker.Application;

public class BugTrackerService
{
  private readonly IBugRepository _repository;

  public BugTrackerService(IBugRepository repository)
  {
    _repository = repository;
  }

  public Bug ReportNew(string title, Severity severity)
  {
    var transient = new Bug(0, title, severity);
    return _repository.Add(transient);
  }

  public bool FixById(int id)
  {
    var bug = _repository.GetById(id);
    if (bug is null) return false;

    bug.Fix();
    _repository.Save(bug);
    return true;
  }

  public List<Bug> GetAll() => _repository.GetAll();

  public List<Bug> GetOpenBugs() =>
    _repository.GetAll().Where(b => !b.IsFixed).ToList();

  public int CountOpenBySeverity(Severity severity) =>
    _repository.GetAll().Count(b => !b.IsFixed && b.Severity == severity);
}
