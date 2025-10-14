using BugTracker.Domain;
using BugTracker.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Tests
{
    public class ConsoleReportTests
    {
        [Theory]
        [InlineData(1, "Login fails", Severity.High, false, "[1] Login fails | High | Open")]
        [InlineData(2, "Tooltip", Severity.Low, true, "[2] Tooltip | Low | Fixed")]
        [InlineData(3, "API 500", Severity.Medium, false, "[3] API 500 | Medium | Open")]
        public void Format_ReturnsExpectedLine(int id, string title, Severity severity, bool isFixed, string expected)
        {
            var bug = new Bug(id, title, severity) { IsFixed = isFixed };

            var line = ConsoleReport.Format(bug);

            Assert.Equal(expected, line);
        }
    }
}
