using BugTracker.Domain;

namespace BugTracker.Infrastructure;

public sealed class BugRepository() : IRepository
{
  private readonly List<Bug> _items = [];
  private int _nextId = 1;

  public Bug Add(Bug bug)
  {
    Bug bugWithId = new(_nextId, bug.Title, bug.Severity);
    _items.Add(bugWithId);
    _nextId++;
    return bugWithId;
  }

  public Bug? GetById(int id)
  {
    for (int i = 0; i < _items.Count; i++)
    {
      if (_items[i].Id == id)
      {
        return _items[i];
      }
    }

    return null;
  }

  public List<Bug> GetAll()
  {
    return _items;
  }

  public void Save(Bug bug)
  {
    // TODO: In-memory only.
  }
}
