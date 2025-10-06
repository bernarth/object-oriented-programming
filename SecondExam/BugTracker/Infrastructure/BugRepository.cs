using BugTracker.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Infrastructure;

public class BugRepository : IBugRepository
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
    Bug bugWithId = new(_nextId, bug.Title, bug.Severity);
    _items.Add(bugWithId);
    _nextId++;
    return bugWithId;
  }

  public Bug? GetById(int id)
  {
    return _items.FirstOrDefault(b => b.Id == id);
  }

  public List<Bug> GetAll()
  {
    return new List<Bug>(_items);
  }

  public void Update(Bug bug)
  {
    int index = _items.FindIndex(b => b.Id == bug.Id);
    if (index != -1)
    {
      _items[index] = bug;
    }
  }
}