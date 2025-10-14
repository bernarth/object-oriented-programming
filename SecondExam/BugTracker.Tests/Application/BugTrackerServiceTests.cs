using BugTracker.Application;
using BugTracker.Domain;
using BugTracker.Infrastructure;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace BugTracker.Tests;

public class BugTrackerServiceTests
{
    private readonly Mock<IBugRepository> _mockRepository;
    private readonly BugTrackerService _service;
    private readonly Fixture _fixture;

    public BugTrackerServiceTests()
    {
        _mockRepository = new Mock<IBugRepository>();
        _service = new BugTrackerService(_mockRepository.Object);
        _fixture = new Fixture();

        _fixture.Register<Bug>(() => new Bug(
            _fixture.Create<int>(),
            _fixture.Create<string>().Substring(0, 10),
            _fixture.Create<Severity>()
        ));
    }

    [Fact]
    public void ReportNew_ValidBug_CallsRepositoryAddAndReturnsBugWithId()
    {
        // Arrange
        string title = _fixture.Create<string>().Substring(0, 10);
        Severity severity = _fixture.Create<Severity>();
        int assignedId = _fixture.Create<int>();

        _mockRepository.Setup(repo => repo.Add(It.IsAny<Bug>()))
                       .Returns((Bug b) => new Bug(assignedId, b.Title, b.Severity));

        var result = _service.ReportNew(title, severity);

        Assert.NotNull(result);
        Assert.Equal(assignedId, result.Id);
        _mockRepository.Verify(repo => repo.Add(It.Is<Bug>(b => b.Title == title && b.Severity == severity)), Times.Once);
    }

    [Fact]
    public void FixById_ExistingOpenBug_MarksAsFixedAndUpdatesRepository()
    {

        int bugId = _fixture.Create<int>();
        var bugToFix = new Bug(bugId, _fixture.Create<string>().Substring(0, 10), _fixture.Create<Severity>());
        
        _mockRepository.Setup(repo => repo.GetById(bugId)).Returns(bugToFix);
        _mockRepository.Setup(repo => repo.Update(It.IsAny<Bug>()));

        _service.FixById(bugId);

        Assert.True(bugToFix.IsFixed);
        _mockRepository.Verify(repo => repo.GetById(bugId), Times.Once);
        _mockRepository.Verify(repo => repo.Update(bugToFix), Times.Once);
    }

    [Fact]
    public void FixById_NonExistingBug_ThrowsBugNotFoundException()
    {
        int nonExistingId = _fixture.Create<int>();
        _mockRepository.Setup(repo => repo.GetById(nonExistingId)).Returns((Bug)null);

        Assert.Throws<BugNotFoundException>(() => _service.FixById(nonExistingId));
        _mockRepository.Verify(repo => repo.GetById(nonExistingId), Times.Once);
        _mockRepository.Verify(repo => repo.Update(It.IsAny<Bug>()), Times.Never);
    }

    [Fact]
    public void GetOpenBugs_ReturnsOnlyBugsThatAreNotFixed()
    {
        var openBug = _fixture.Create<Bug>();
        var fixedBug = _fixture.Create<Bug>();
        fixedBug.Fix();
        var anotherOpenBug = _fixture.Create<Bug>();

        var allBugs = new List<Bug> { openBug, fixedBug, anotherOpenBug };
        _mockRepository.Setup(repo => repo.GetAll()).Returns(allBugs);

        var result = _service.GetOpenBugs();

         Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.Id == openBug.Id);
        Assert.DoesNotContain(result, b => b.Id == fixedBug.Id);
        Assert.Contains(result, b => b.Id == anotherOpenBug.Id);
        _mockRepository.Verify(repo => repo.GetAll(), Times.Once);
    }
}