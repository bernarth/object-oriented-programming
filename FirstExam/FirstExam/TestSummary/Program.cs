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

var app = new App(new CsvReader(), new Analyzer(), new Reporter());
app.Run(filePath, notify);