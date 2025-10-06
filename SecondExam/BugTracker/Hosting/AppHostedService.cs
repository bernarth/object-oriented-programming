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

    bool isFixed = _service.FixById(bug2.Id);

    if (isFixed)
    {
      Console.WriteLine("Bug fixed successfully.");
    }
    else
    {
      Console.WriteLine("Bug not found.");
    }

    Console.WriteLine();
    Console.WriteLine("=== All Bugs ===");
    ConsoleReport.Print(_service.GetAll());

    Console.WriteLine();
    Console.WriteLine("=== Open Bugs ===");
    ConsoleReport.Print(_service.GetOpenBugs());

    int openHigh = _service.CountOpenBySeverity(Severity.High);
    Console.WriteLine();
    Console.WriteLine("Open High severity count: " + openHigh);

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
