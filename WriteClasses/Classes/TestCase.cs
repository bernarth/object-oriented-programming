namespace Classes;

public class TestCase(string title, Func<Task> action, PriorityLevel? priority = null)
{
  // Caracteristics
  public const int MaxTitleLength = 120;
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
  // SOLVED: Change the priority to an enum: P1, P2, P3, P4. Add validations if necessary.
  public enum PriorityLevel { P1, P2, P3, P4 }
  public PriorityLevel Priority { get; set; } = priority ?? PriorityLevel.P3;
  // SOLVED: Change the status to an enum: NotRun, Passed, Failed
  public enum TestStatus { NotRun, Passed, Failed }
  public TestStatus Status { get; private set; } = TestStatus.NotRun;
  public string? FailureReason { get; private set; }
  public Func<Task> Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

  // Behavior
  public bool IsHighPriority => Priority == PriorityLevel.P1;

  public void MarkPassed() => Status = TestStatus.Passed;
  // SOLVED: Add validation for reason.
  public void MarkFailed(string reason)
  {
    if (string.IsNullOrWhiteSpace(reason))
      throw new ArgumentException("Failure reason cannot be empty.", nameof(reason));
    Status = TestStatus.Failed;
    FailureReason = reason;
  }

  public async Task ExecuteAsync()
  {
    await Action();
    // we can have logic here to grab the logs or something.
  }
}