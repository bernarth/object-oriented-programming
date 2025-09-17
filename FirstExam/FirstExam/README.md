## “Failing Test Summary”

What it does (current behavior)

- Reads a CSV: Suite,TestName,Status,DurationMs,Timestamp

- Prints:

  - Total tests

  - Passed / Failed counts

  - A de-duplicated list of failing tests (Suite/TestName)

- If `--notify` is present, prints a fake “notification” line (It's not part of the test)

- It is not mandatory that the project runs

Keep this exact behavior after refactor.

## How to run the project

`dotnet run --project TestSummary -- --file test-results.csv --notify`