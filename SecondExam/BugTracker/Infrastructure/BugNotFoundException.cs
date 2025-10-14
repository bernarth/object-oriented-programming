using System;

namespace BugTracker.Infrastructure;

public class BugNotFoundException : Exception
{
    public BugNotFoundException() { }
    public BugNotFoundException(string message) : base(message) { }
    public BugNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}