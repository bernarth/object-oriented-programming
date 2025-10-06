using System.Text.Json;
using BugTracker.Domain;

namespace BugTracker.Infrastructure;

public class BugRepository : IBugRepository
{
  private readonly List<Bug> _items = [];
  private int _nextId = 1;
  private readonly string _filePath;

  public BugRepository(string filePath = "bugs.json")
  {
    _filePath = filePath;
    Load();
  }

  public Bug Add(Bug bug)
  {
    var bugWithId = new Bug(_nextId++, bug.Title, bug.Severity);
    _items.Add(bugWithId);
    return bugWithId;
  }

  public Bug? GetById(int id) => _items.FirstOrDefault(b=> b.Id == id);

  public List<Bug> GetAll() => _items.ToList();

  public void Save(Bug bug)
  {
    var index = _items.FindIndex(b => b.Id == bug.Id);
    if (index >= 0)
    {
      _items[index] = bug;
      SaveToFile();
    }
  }

  private void SaveToFile()
  {
    var options = new JsonSerializerOptions { WriteIndented = true };
    var json = JsonSerializer.Serialize(_items, options);
    File.WriteAllText(_filePath, json);
  }

  private void Load()
  {
    if (!File.Exists(_filePath)) return;
    try
    {
      var json = File.ReadAllText(_filePath);
      var bugs = JsonSerializer.Deserialize<List<Bug>>(json);
      if (bugs is not null)
      {
        _items.AddRange(bugs);
        _nextId = _items.Any() ? _items.Max(b => b.Id) +1 : 1;
      }
    }
    catch
    {
      _items.Clear();
      _nextId = 1;
    }
  }
}
