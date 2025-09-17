public class Reader
{
    public string FilePath {get; set;} = "test-results.csv";
    public bool Notify {get; set;} = false;

    public static AppReader Parse(string[] args)
    {
        var output = new Reader();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--file" && i + 1 < args.Length)
            {
                FilePath = args[i + 1];
            }

            if (args[i] == "--notify")
            {
                Notify = true;
            }

            if (notify)
            {
            Console.WriteLine();
            Console.WriteLine("NOTIFY => #qa-alerts | failed=" + failed + " | unique failing tests=" + failingTests.Count);
            }

        }

        if (!File.Exists(FilePath))
        {
        Console.WriteLine("FILE_NOT_FOUND " + FilePath);
        return;
        }

    }
}