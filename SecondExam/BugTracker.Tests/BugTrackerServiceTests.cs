using BugTracker.Application;
using BugTracker.Domain;
using BugTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Tests
{
    public class BugTrackerServiceTests
    {
        [Fact]
        public void ReportNewBug()
        {
            var repository = new BugRepository();
            var service = new BugTrackerService(repository);

            var saved = service.ReportNew("Login null ref", Severity.High);

            Assert.True(saved.Id > 0);
            Assert.Equal("Login null ref", saved.Title);
            Assert.Equal(Severity.High, saved.Severity);

            var all = service.GetAll();
            Assert.Single(all);
        }

        [Fact]
        public void FixById()
        {
            var repository = new BugRepository();
            var service = new BugTrackerService(repository);
            var bug = service.ReportNew("Tooltip issue", Severity.Low);

            var fixedBug = service.FixById(bug.Id);

            Assert.True(fixedBug);

            var actual = repository.GetById(bug.Id);
            Assert.NotNull(actual);
            Assert.True(actual.IsFixed);
        }
    }
}
