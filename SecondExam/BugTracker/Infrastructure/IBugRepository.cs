using BugTracker.Domain;

namespace BugTracker.Infrastructure;

public interface IBugRepository
{
    Bug Add(Bug bug);
    Bug? GetById(int id);
    List<Bug> GetAll();
    void Update(Bug bug);
}