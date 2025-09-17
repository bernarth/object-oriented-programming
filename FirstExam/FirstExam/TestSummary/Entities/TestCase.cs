
public class TestCase(string testSuite, string testName, string status, string durationMs, DateTime timestamp)
{
    public enum StatusEnum
    {
        PASSED,
        FAILED,
        SKIPPED
    }
    public string TestSuite {get;} = testSuite;
    public string TestName {get;} = testName;
    public StatusEnum Status {get;} = status;
    public string DurationMs {get;} = durationMs;
    public DateTime Timestamp {get;} = timestamp;

    public TestCase testCSV(string[] args){

    }
}