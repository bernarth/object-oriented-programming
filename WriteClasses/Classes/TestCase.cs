namespace Classes;

public enum PriorityLevel
{
  P1,
  P2,
  P3,
  P4
}

public enum TestStatus
{
  NotRun,
  Passed,
  Failed
}

public class TestCase(string title, Func<Task> action, string? priority = null)
{
  // Caracteristics
  public const int MaxTitleLength = 120;
  private const string DefaultPriority = "P3";

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
  public PriorityLevel Priority { get; set; } = priority ?? DefaultPriority;
  // TODO: Change the status to an enum: NotRun, Passed, Failed
  public TestStatus Status { get; private set; } = TestStatus.NotRun;
  public string? FailureReason { get; private set; }
  public Func<Task> Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

  // Behavior
  public bool IsHighPriority => Priority == PriorityLevel.P1;

  public void MarkPassed()
    {
      Status = TestStatus.Passed;
      FailureReason = null;
    }
  // TODO: Add validation for reason.
  public void MarkFailed(string reason)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(reason, nameof(reason));

      Status = TestStatus.Failed;
      FailureReason = reason.Trim();
    }

  public async Task ExecuteAsync()
  {
    await Action();
    // we can have logic here to grab the logs or something.
  }
}
