
namespace TestSummary
{
    public class CliOptions
    {
        public string FilePath { get; }

        public bool Notify {  get; }

        public CliOptions(string filePath, bool notify) 
        {
            FilePath = filePath;
            Notify = notify;
        } 

        public CliOptions Parse(string[] args)
        {
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

            return new CliOptions(filePath, notify);
        }
    }
}
