using TestSummary.Entities;

namespace TestSummary;

public class CsvReader
{
  public IEnumerable<TestResult> ReadAll(string filePath)
  {
    if (!File.Exists(filePath))
    {
      Console.WriteLine("FILE_NOT_FOUND " + filePath);

      yield break;
    }

    var lines = File.ReadAllLines(filePath);

    foreach (var line in lines.Skip(1))
    {
      var parts = line.Split(',');

      if (parts.Length != 5)
      {
        continue;
      }

      yield return new TestResult
      {
        Suite = parts[0],
        TestName = parts[1],
        Status = Enum.Parse<Status>(parts[2], true),
        DurationMs = int.Parse(parts[3]),
        Timestamp = DateTime.Parse(parts[4])
      };
    }
  }
}