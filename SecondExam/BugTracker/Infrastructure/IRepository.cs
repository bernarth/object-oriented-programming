using BugTracker.Domain;
namespace BugTracker.Infrastructure;

public interface IRepository
{
  Bug Add(Bug bug);
  Bug? GetById(int id);
  List<Bug> GetAll();
  void Save(Bug bug);
}