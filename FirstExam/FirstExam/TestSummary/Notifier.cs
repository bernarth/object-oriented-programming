namespace TestSummary
{
    public class Notifier
    {
        public void Notify(TestSummary testSummary)
        {
            Console.WriteLine();
            Console.WriteLine("NOTIFY => #qa-alerts | failed=" + testSummary.Failed + " | unique failing tests=" + testSummary.FailingTests.Count);
        }
    }
}
