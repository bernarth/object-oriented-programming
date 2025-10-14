using BugTracker.Domain;

namespace BugTracker.Infrastructure;

public class BugRepository
{
  private readonly List<Bug> _items;
  private int _nextId;

  public BugRepository()
  {
    _items = [];
    _nextId = 1;
  }

  public Bug Add(Bug bug)
  {
        Bug bugWithId = new(_nextId, bug.Title, bug.Severity)
        {
            IsFixed = bug.IsFixed,
        };

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
    for(int i = 0; i < _items.Count; i++)
    {
       if (_items[i].Id == bug.Id)
       {
          _items[i].Title = bug.Title;
          _items[i].Severity = bug.Severity;
          _items[i].IsFixed = bug.IsFixed;

                return;
       }
    }  
  }
}
