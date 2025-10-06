using System;

namespace BugTracker.Domain
{
    public class Bug
    {
        public int Id { get; init; }
        public string Title { get; private set; }
        public Severity Severity { get; init; }
        public bool IsFixed { get; private set; }

        public Bug(int id, string title, Severity severity)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be negative.");
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Title must not be empty.", nameof(title)) : title;
            Id = id;
            Severity = severity;
            IsFixed = false;
        }

        public void Fix() => IsFixed = true;

        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle)) throw new ArgumentException("New title must not be empty.", nameof(newTitle));
            Title = newTitle;
        }

        public override string ToString() =>
            $"[{Id}] {Title} | {Severity} | {(IsFixed ? "Fixed" : "Open")}";
    }
}
