using System.Text;
using BugTracker.Domain;

namespace BugTracker.Presentation;

public static class ConsoleFormatter
{
    public static string Format(Bug bug)
    {
        var builder = new StringBuilder();
        builder.Append($"[{bug.Id}] {bug.Title} | {bug.Severity} | {(bug.IsFixed ? "Fixed" : "Open")}");

        return builder.ToString();
    }
}
