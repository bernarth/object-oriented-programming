using BugTracker.Domain;
using BugTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Application;

public class BugTrackerService(IBugRepository repository)
{
  private readonly IBugRepository _repository = repository;

  public Bug ReportNew(string title, Severity severity)
  {
    if (string.IsNullOrWhiteSpace(title))
    {
      throw new ArgumentException("Bug title cannot be empty.", nameof(title));
    }

    Bug transient = new(0, title, severity);
    Bug saved = _repository.Add(transient);
    return saved;
  }
  public void FixById(int id)
  {
    if (id <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(id), "Bug ID must be a positive integer.");
    }

    Bug? bug = _repository.GetById(id);
    if (bug == null)
    {
      throw new BugNotFoundException($"Bug with ID {id} not found.");
    }

    bug.Fix();
    _repository.Update(bug);
  }

  public void RenameBug(int id, string newTitle)
  {
      if (id <= 0)
      {
          throw new ArgumentOutOfRangeException(nameof(id), "Bug ID must be a positive integer.");
      }
      if (string.IsNullOrWhiteSpace(newTitle))
      {
          throw new ArgumentException("New title cannot be null or empty.", nameof(newTitle));
      }

      Bug? bug = _repository.GetById(id);
      if (bug == null)
      {
          throw new BugNotFoundException($"Bug with ID {id} not found.");
      }

      bug.Rename(newTitle);
      _repository.Update(bug);
  }
  public List<Bug> GetAll()
  {
    return _repository.GetAll();
  }
  public List<Bug> GetOpenBugs()
  {
    return _repository.GetAll().Where(bug => !bug.IsFixed).ToList();
  }

  public int CountOpenBySeverity(Severity severity)
  {
    return _repository.GetAll()
                      .Where(bug => !bug.IsFixed && bug.Severity == severity)
                      .Count();
  }
}