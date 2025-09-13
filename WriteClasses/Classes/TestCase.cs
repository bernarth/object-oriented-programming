namespace Classes;

public class TestCase(string title, Func<Task> action, string? priority = null)
{
  // TODO: Change the priority to an enum: P1, P2, P3, P4. Add validations if necessary.
private enum TestPriorities
{
  P1,
  P2,
  P3,
  P4
}
  // TODO: Change the status to an enum: NotRun, Passed, Failed
public enum TestStatus
{
  NotRun,
  Passed,
  Failed,
}

  // Caracteristics
  public const int MaxTitleLength = 120;
  private const TestPriorities DefaultPriority = TestPriorities.P3;

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
  private TestPriorities Priority { get; set; } = priority ?? DefaultPriority;
  public TestStatus Status { get; private set; } = TestStatus.NotRun;
  public string? FailureReason { get; private set; }
  public Func<Task> Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

  // Behavior
  public bool IsHighPriority => Priority is TestPriorities.P1;

  public void MarkPassed() => (Status, FailureReason) = (TestStatus.Passed, null);
   
  // TODO: Add validation for reason.
  public void MarkFailed(string reason) => (Status, FailureReason) = (TestStatus.Failed, reason);

  public async Task ExecuteAsync()
  {
    await Action();
    // we can have logic here to grab the logs or something.
  }
}