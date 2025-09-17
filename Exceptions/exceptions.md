# Exception Handling - Manage Errors Friendly

## Exception Types

### Definition / Theory

Exceptions are unexpected conditions that interrupt normal control flow. In .NET (C#), all the exceptions inherit from `System.Exceptions`. Thanks to that we can specify exceptions types that we want to handle (for example: because of diagnositcs, logs, enable catching)

Common exception types:

- `ArgumentNullException`, `ArgumentOutOfRangeException`: Invalid input parameters for methods or arrays.
- `InvalidOperationException`: Method called in an invalid object state.
- `FormatException`: parsing/formmatting issues (for example: CSV, json)
- `TimeoutException` or `TaskCanceledException` - timeouts or cancellations
- `HttpRequestException`: HTTP failures from API calls
- `IOExcetion`: File system problems.
- `NotSupportedException`: feature not implemented or supported.

Note: Avoid manually `NullReferenceException`, `IndexOutOfRangeException` they typically comes from the framework.
