using System.Collections.Generic;
using BugTracker.Domain;

public interface IBugRepository
{
    List<Bug> GetAll();
    Bug? GetById(int id);
    Bug Add(Bug bug);
    void Save(Bug bug);
}