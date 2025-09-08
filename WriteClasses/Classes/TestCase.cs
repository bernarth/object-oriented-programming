namespace Classes;

public enum PriorityEnum { P1, P2, P3, P4 };
public enum StatusEnum { NotRun, Passed, Failed };
public class TestCase(string title, Func<Task> action, PriorityEnum priority = PriorityEnum.P3)
{
  // Caracteristics
  public const int MaxTitleLength = 120;
  public const int MaxReasonLength = 150;
  private string _title = title;
  private string? _reason;
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
  public PriorityEnum Priority { get; set; } = priority;
  // SOLVED: Change the status to an enum: NotRun, Passed, Failed
  public StatusEnum Status { get; private set; }
  public string? FailureReason
  {
    get => _reason; private set
    {

      if (value != null && value.Length > MaxReasonLength)
      {
        throw new ArgumentOutOfRangeException(nameof(FailureReason), $"Max {MaxReasonLength} chars.");
      }

      _reason = value;
    }
  }
  public Func<Task> Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

  // Behavior
  public bool IsHighPriority => Priority is PriorityEnum.P1;

  public void MarkPassed() => Status = StatusEnum.Passed;
  // SOLVED: Add validation for reason.
  public void MarkFailed(string? reason=null) => (Status, FailureReason) = (StatusEnum.Failed, reason);

  public async Task ExecuteAsync()
  {
    await Action();
    // we can have logic here to grab the logs or something.
  }
}