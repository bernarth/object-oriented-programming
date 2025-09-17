public class TestResult
{
    public string Suite { get; }
    public string TestName { get; }
    public string Status { get; }

    public TestResult(string suite, string testName, string status)
    {
        Suite = suite;
        TestName = testName;
        Status = status;
    }
}