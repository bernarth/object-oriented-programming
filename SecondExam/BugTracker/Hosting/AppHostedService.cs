using BugTracker.Application;
using BugTracker.Domain;
using BugTracker.Presentation;
using Microsoft.Extensions.Hosting;

namespace BugTracker.Hosting;

public class AppHostedService : IHostedService
{
  private readonly BugTrackerService _service;

  public AppHostedService(BugTrackerService service)
  {
    _service = service;
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    // [OPTIONAL]: Add arguments
    var bug1 = _service.ReportNew("Login page crashes on empty password", Severity.High);
    var bug2 = _service.ReportNew("Tooltip overlaps with button", Severity.Low);
    var bug3 = _service.ReportNew("API returns 500 for invalid token", Severity.High);

    var isFixed = _service.FixById(bug2.Id);
    Console.WriteLine(isFixed ? "Bug fixed successfully." : "Bug not found.");
    
    Console.WriteLine();
    Console.WriteLine("=== All Bugs ===");
    ConsoleReport.Print(_service.GetAll());

    Console.WriteLine();
    Console.WriteLine("=== Open Bugs ===");
    ConsoleReport.Print(_service.GetOpenBugs());

    var openHigh = _service.CountOpenBySeverity(Severity.High);
    Console.WriteLine();
    Console.WriteLine($"Open High severity count: {openHigh}");

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken) =>
    Task.CompletedTask;
}
