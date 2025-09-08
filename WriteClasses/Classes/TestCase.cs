namespace Classes;

public enum TestPriority
{
  P1, // Critical
  P2, // High
  P3, // Medium
  P4  // Low
}

public class TestCase(string title, Func<Task> action, TestPriority? priority = null)
{
  // Caracteristics
  public const int MaxTitleLength = 120;
  private const TestPriority DefaultPriority = TestPriority.P3;

  private string _title = title;

  public Guid Id { get; } = Guid.NewGuid();
  public string Title
  {
    get => _title;
    set
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(value);

      if (value.Length > MaxTitleLength)
      {
        throw new ArgumentOutOfRangeException(nameof(Title), $"Max {MaxTitleLength} chars.");
      }

      _title = value.Trim();
    }
  }
  // TODO: Change the priority to an enum: P1, P2, P3, P4. Add validations if necessary.
  public TestPriority Priority { get; set; } = priority ?? DefaultPriority;
  // TODO: Change the status to an enum: NotRun, Passed, Failed
  public string Status { get; private set; } = "NotRun";
  public string? FailureReason { get; private set; }
  public Func<Task> Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

  // Behavior
  public bool IsHighPriority => Priority is TestPriority.P1;

  public void MarkPassed() => Status = "Passed";
  public void MarkFailed(string? reason) 
  {
    if (reason != null && reason.Length > 150)
    {
      reason = reason.Substring(0, 150); 
    }
    (Status, FailureReason) = ("Failed", reason);
  }

  public async Task ExecuteAsync()
  {
    await Action();
    // we can have logic here to grab the logs or something.
  }
}