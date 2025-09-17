using System.Collections.Generic;

public class TestSummary
{
    public int Total { get; set; }
    public int Passed { get; set; }
    public int Failed { get; set; }
    public List<string> PassingTests { get; } = new List<string>();
    public List<string> FailingTests { get; } = new List<string>();
}