using BugTracker.Application;
using BugTracker.Domain;
using BugTracker.Presentation;
using Microsoft.Extensions.Hosting;

namespace BugTracker.Hosting;

public class AppHostedService : IHostedService
{
  private readonly IBugTrackerService _service;

  public AppHostedService(IBugTrackerService service)
  {
    _service = service;
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    // [OPTIONAL]: Add arguments
    Bug bug1 = _service.ReportNew("Login page crashes on empty password", Severity.High);
    Bug bug2 = _service.ReportNew("Tooltip overlaps with button", Severity.Low);
    Bug bug3 = _service.ReportNew("API returns 500 for invalid token", Severity.High);

    while (true)
    {
      Console.WriteLine("\n--- Bug Tracker Menu ---");
      Console.WriteLine("1. Insert a new Bug");
      Console.WriteLine("2. List all existent Bugs");
      Console.WriteLine("3. Fix a bug by ID");
      Console.WriteLine("4. Exit");
      Console.Write("Choose an option: ");
      string? option = Console.ReadLine();

      switch (option)
      {
        case "1":
          Console.Write("Enter bug title: ");
          string title = Console.ReadLine() ?? "";
          Console.Write("Enter severity (Low, Medium, High): ");
          string severityInput = Console.ReadLine() ?? "Low";
          Severity severity = Enum.TryParse(severityInput, true, out Severity sev) ? sev : Severity.Low;
          Bug bug = _service.ReportNew(title, severity);
          Console.WriteLine($"Bug created: [{bug.Id}] {bug.Title} | {bug.Severity}");
          break;

        case "2":
          Console.WriteLine();
          Console.WriteLine("=== All Bugs ===");
          ConsoleReport.Print(_service.GetAll());

          Console.WriteLine();
          Console.WriteLine("=== Open Bugs ===");
          ConsoleReport.Print(_service.GetOpenBugs());

          int openHigh = _service.CountOpenBySeverity(Severity.High);
          Console.WriteLine();
          Console.WriteLine("Open High severity count: " + openHigh);
          break;

        case "3":
          Console.Write("Enter bug ID to fix: ");
          if (int.TryParse(Console.ReadLine(), out int fixId))
          {
            bool isFixed = _service.FixById(fixId);
            Console.WriteLine(isFixed ? "Bug fixed successfully." : "Bug not found.");
          }
          else
          {
            Console.WriteLine("Invalid ID.");
          }
          break;

        case "4":
          Console.WriteLine("Exiting...");
          return Task.CompletedTask;

        default:
          Console.WriteLine("Invalid option. Try again.");
          break;
      }
    }
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
