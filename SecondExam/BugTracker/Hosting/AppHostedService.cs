using BugTracker.Application;
using BugTracker.Domain;
using BugTracker.Infrastructure;
using BugTracker.Presentation;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BugTracker.Hosting;

public class AppHostedService(BugTrackerService service) : IHostedService
{
  private readonly BugTrackerService _service = service;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    try
    {
        Bug bug1 = _service.ReportNew("Login page crashes on empty password", Severity.High);
        Bug bug2 = _service.ReportNew("Tooltip overlaps with button", Severity.Low);
        Bug bug3 = _service.ReportNew("API returns 500 for invalid token", Severity.High);
        Bug bug4 = _service.ReportNew("Unresponsive button on checkout", Severity.Medium);

        Console.WriteLine($"Reported bug: {ConsoleReport.Format(bug1)}");
        Console.WriteLine($"Reported bug: {ConsoleReport.Format(bug2)}");
        Console.WriteLine($"Reported bug: {ConsoleReport.Format(bug3)}");
        Console.WriteLine($"Reported bug: {ConsoleReport.Format(bug4)}");
        Console.WriteLine();

        try
        {
            _service.FixById(bug2.Id);
            Console.WriteLine($"Bug {bug2.Id} fixed successfully.");
        }
        catch (BugNotFoundException ex)
        {
            Console.WriteLine($"Error fixing bug: {ex.Message}");
        }
        catch (InvalidBugStateException ex)
        {
            Console.WriteLine($"Error fixing bug: {ex.Message}");
        }

        try
        {
            _service.FixById(bug2.Id);
            Console.WriteLine($"Bug {bug2.Id} fixed successfully (should not happen).");
        }
        catch (BugNotFoundException ex)
        {
            Console.WriteLine($"Error fixing bug: {ex.Message}");
        }
        catch (InvalidBugStateException ex)
        {
            Console.WriteLine($"Correctly caught: {ex.Message}");
        }

        try
        {
            _service.RenameBug(bug4.Id, "Checkout button is sometimes unresponsive");
            Console.WriteLine($"Bug {bug4.Id} renamed successfully.");
        }
        catch (BugNotFoundException ex)
        {
            Console.WriteLine($"Error renaming bug: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error renaming bug (validation): {ex.Message}");
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
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unhandled error occurred in AppHostedService: {ex.Message}");
    }


    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}