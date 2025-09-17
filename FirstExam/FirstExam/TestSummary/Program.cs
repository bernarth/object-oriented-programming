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

if (!File.Exists(filePath))
{
  Console.WriteLine("FILE_NOT_FOUND " + filePath);
  return;
}

// Globals / shared mutable state (bad on purpose)
var lines = File.ReadAllLines(filePath).ToList();
var rows = new List<string[]>();
int total = 0;
int passed = 0;
int failed = 0;
var failingTests = new List<string>();
var seenFail = new HashSet<string>();

// Poor man's CSV (no quoting, no culture handling)
for (int i = 0; i < lines.Count; i++)
{
  var l = lines[i].Trim();
  if (l.Length == 0) continue;
  if (i == 0 && l.StartsWith("Suite,TestName,Status")) continue; // skip header
  var parts = l.Split(',');

  if (parts.Length < 5)
  {
    continue;
  }

  rows.Add(parts);
}

// Mix parsing, counting, and output concerns in one place
foreach (var r in rows)
{
  total++;
  var suite = r[0].Trim();
  var test = r[1].Trim();
  var status = r[2].Trim().ToUpperInvariant();
  // duration (r[3]) and timestamp (r[4]) ignored in this tiny version

  if (status == "PASS") passed++;
  else if (status == "FAIL")
  {
    failed++;
    var key = suite + "/" + test;

    if (!seenFail.Contains(key))
    {
      failingTests.Add(key);
      seenFail.Add(key);
    }
  }
}

Console.WriteLine("==== Test Summary ====");
Console.WriteLine("File: " + filePath);
Console.WriteLine("Total: " + total);
Console.WriteLine("Passed: " + passed);
Console.WriteLine("Failed: " + failed);
Console.WriteLine();
Console.WriteLine("Failing Tests:");

if (failingTests.Count == 0)
{
  Console.WriteLine("(none)");
}
else
{
  foreach (var t in failingTests.OrderBy(x => x))
  {
    Console.WriteLine("- " + t);
  }
}

if (notify)
{
  Console.WriteLine();
  Console.WriteLine("NOTIFY => #qa-alerts | failed=" + failed + " | unique failing tests=" + failingTests.Count);
}
