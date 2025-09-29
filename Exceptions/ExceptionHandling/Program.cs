using ExceptionHandling;

static void LoadFromCsv(string csvPath)
{
  if (string.IsNullOrWhiteSpace(csvPath))
  {
    throw new CustomException("CSV path is required :)");
  }

  var lines = File.ReadAllLines(csvPath);

  var result = string.Join('\n', lines);

  Console.WriteLine(result);
}

try
{
  Console.WriteLine("==== MY SYSTEM ====");
  LoadFromCsv(null!);
}
catch (ArgumentException)
{
  Console.WriteLine("CSV path is required :)");
}
catch (Exception exception)
{
  Console.WriteLine(exception.Message);
}
finally
{
  Console.WriteLine("The task has been finished!");
}