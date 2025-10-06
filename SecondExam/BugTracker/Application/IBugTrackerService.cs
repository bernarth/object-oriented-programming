using BugTracker.Domain;

namespace BugTracker.Application
{
    public interface IBugTrackerService
    {
        Bug ReportNew(string title, Severity severity);
        bool FixById(int id);
        List<Bug> GetAll();
        List<Bug> GetOpenBugs();
        int CountOpenBySeverity(Severity severity);
    }
}