namespace TestSummary
{
    public class FileReader
    {
        public bool Exists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("FILE_NOT_FOUND " + filePath);
                return false;
            }
            return true;
        }

        public List<string[]> ParseRows(string filePath)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            var rows = new List<string[]>();
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
            return rows;
        }
    }
}
