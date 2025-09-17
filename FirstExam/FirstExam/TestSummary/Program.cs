string filePath = "test-results.csv";

bool notify = false;

for (int i = 0; i < args.Length; i++)
{
  if (args[i] == "--file" && i + 1 < args.Length)
  {
    filePath = args[i + 1];
  }

  if (args[i] == "--notify")
  {
    notify = true;
  }
}
var file=new FileReader();
if !file.FileExist(){
Console.WriteLine(file.ErrorMessage); 
}
var content = file.ReadFile();



// Classes

public class FileReader(string filePath="test-results.csv")
{
  private readonly string _filePath = filePath;
  public  string ErrorMessage {get; set;}

  public List<string> ReadFile()
  {
    return File.ReadAllLines(_filePath).ToList();
  }

  public bool FileExist()
  {
    this.ErrorMessage= File.Exists(_filePath)? "": "FILE_NOT_FOUND " + _filePath;
    return File.Exists(_filePath);  
  }
}

public class TestResult
{
  public string Suite {get; set;}
  public string TestName {get; set;}
  public string Status {get; set;}

  public string GetData()
  {
    return $"{Suite}/{TestName}"
  }
}

public class CsvParser(List<string> lines)
{
  private readonly List<string> _lines = lines;
  private readonly List<TestResult> _results=[];

  public List<string> Parse(List<TestResult> testResult)
  {
    foreach (var line in _lines)
    {
      var l = lines.Trim();
      if (l.Length == 0 && l.StartsWith("Suite,TestName,Status")) continue; // skip header
      var parts = l.Split(',');
      _results.Add(new TestResult(parts[0].Trim,parts[1].Trim,parts[2].Trim().ToUpperInvariant()))
    }
  }
  return _results
}

public class Summarizer()
{
  public int Total {get; set;}
  public int Passed {get; set;}
  public int Failed {get; set;}

  public void GenerateSummary(List<TestResult> results)
  {
    foreach (var result in results)
    {
      Total++;
      if (result.Status == "PASS") 
      {
        Passed++;
      }
      else if (result.Status == "FAIL")
      {
        Failed++;
      }
    }
  }
}



