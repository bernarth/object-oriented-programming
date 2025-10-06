using System;

namespace BugTracker.Domain;

public class InvalidBugStateException : Exception
{
    public InvalidBugStateException() { }
    public InvalidBugStateException(string message) : base(message) { }
    public InvalidBugStateException(string message, Exception innerException) : base(message, innerException) { }
}