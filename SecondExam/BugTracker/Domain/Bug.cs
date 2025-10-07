namespace BugTracker.Domain;

public class Bug(int id, string title, Severity severity)
{
  public int Id { get; set; } = id;
  public string Title { get; set; } = title;
  public Severity Severity { get; set; } = severity;
  public bool IsFixed { get; set; } = false;
  public bool Fix() => IsFixed = true;

  public void Rename(string newTitle)
  {
    Title = newTitle;
  }
}
