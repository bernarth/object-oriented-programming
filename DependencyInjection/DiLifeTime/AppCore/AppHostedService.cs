using DiLifeTime.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiLifeTime.AppCore;

public class AppHostedService(IServiceScopeFactory scopeFactory) : IHostedService
{
  /// <summary>
  /// We are going to use IServiceScopeFactory to simulate:
  /// - Calls to an API
  /// - Calls to services in an Blazor application
  /// - Renders with MAUI
  /// - Scoped calls to services
  /// </summary>
  private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    // We start a scope 
    using (var scope1 = _scopeFactory.CreateScope())
    {
      Console.WriteLine("=== Scope 1 ===");
      FillNumbers(scope1.ServiceProvider, 1, 2, 3);
      ShowNumbers(scope1.ServiceProvider);
    }

    // We start a Second Scope
    using (var scope2 = _scopeFactory.CreateScope())
    {
      Console.WriteLine("=== Scope 2 ===");
      FillNumbers(scope2.ServiceProvider, 4, 5);
      ShowNumbers(scope2.ServiceProvider);
    }

    Console.WriteLine("Demo finished.");
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Console.WriteLine("Application stopping...");
    return Task.CompletedTask;
  }

  private static void FillNumbers(IServiceProvider serviceProvider, params int[] values)
  {
    serviceProvider.GetRequiredService<NumberStorageSingleton>().Numbers.AddRange(values);
    serviceProvider.GetRequiredService<NumberStorageScoped>().Numbers.AddRange(values);
    serviceProvider.GetRequiredService<NumberStorageTransient>().Numbers.AddRange(values);
  }

  private static void ShowNumbers(IServiceProvider serviceProvider)
  {
    var singleton = serviceProvider.GetRequiredService<NumberStorageSingleton>();
    var scoped = serviceProvider.GetRequiredService<NumberStorageScoped>();
    var transient = serviceProvider.GetRequiredService<NumberStorageTransient>();

    Console.WriteLine($"Singleton numbers : [{string.Join(", ", singleton.Numbers)}]");
    Console.WriteLine($"Scoped numbers    : [{string.Join(", ", scoped.Numbers)}]");
    Console.WriteLine($"Transient numbers : [{string.Join(", ", transient.Numbers)}]");
    Console.WriteLine();
  }
}
