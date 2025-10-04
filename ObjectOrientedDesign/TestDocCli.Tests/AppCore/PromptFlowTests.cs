using AutoFixture;
using Moq;
using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.InputOutput;
using TestDocCli.Model;

namespace TestDocCli.Tests.AppCore;

public class PromptFlowTests
{
  private readonly Mock<IConsole> _consoleMock = new(MockBehavior.Strict);
  private readonly Mock<IInputReader> _readerMock = new(MockBehavior.Strict);
  private readonly Fixture _fixture = new();

  public PromptFlowTests()
  {
    _consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Verifiable();
  }

  [Fact]
  public void CollectTestDocument_WithoutMoq_ShouldCaptureConsole()
  {
    var console = new FakeConsole();
    var reader = new FakeInputReader(
      new Dictionary<string, string>
      {
        { "Title", "Checkout" },
        { "Description", "User can checkout" },
        { "Expected Result", "Order completed" },
        { "Actual Result", "Order un-completed" }
      },
      ["Add to cart", "Pay"]
    );
    var promptFlow = new PromptFlow(reader, console);

    TestDocument result = promptFlow.CollectTestDocument();

    Assert.Equal("Checkout", result.Title);
    Assert.Equal(2, result.Steps.Count);
    Assert.Contains("Test Documentation Generator", console.JoinedOutput);
  }

  private sealed class FakeConsole : IConsole
  {
    private readonly List<string> _lines = [];
    public string JoinedOutput => string.Join('\n', _lines);

    public string ReadLine()
    {
      return string.Empty;
    }

    public void Write(string message)
    {
      _lines.Add(message);
    }

    public void WriteLine(string message)
    {
      _lines.Add(message);
    }
  }

  private sealed class FakeInputReader(Dictionary<string, string> answerByLabel, List<string> steps) : IInputReader
  {
    private readonly Dictionary<string, string> _answerByLabel = answerByLabel;
    private readonly List<string> _steps = steps;

    public string ReadLine(string prompt)
    {
      if (_steps.Count == 0)
      {
        return string.Empty;
      }

      var first = _steps.First();
      _steps.RemoveAt(_steps.Count - 1);

      return first;
    }

    public string ReadRequired(string label)
    {
      if (_answerByLabel.TryGetValue(label, out string? value))
      {
        return value;
      }

      throw new InvalidOperationException($"No answer for label: {label}");
    }
  }

  [Fact]
  public void CollectTestDocument_WithMoq_ShouldCaptureConsole()
  {
    // var consoleMock = new Mock<IConsole>(MockBehavior.Strict);
    // var _readerMock = new Mock<IInputReader>(MockBehavior.Strict);

    _readerMock.Setup(x => x.ReadRequired("Title")).Returns("Checkout");
    _readerMock.Setup(x => x.ReadRequired("Description")).Returns("User can checkout");
    // consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Verifiable();
    _readerMock.SetupSequence(x => x.ReadLine(It.Is<string>(s => s.StartsWith("Step "))))
      .Returns("Add to cart")
      .Returns("Pay")
      .Returns(string.Empty);
    _readerMock.Setup(x => x.ReadRequired("Expected Result")).Returns("Order completed");
    _readerMock.Setup(x => x.ReadRequired("Actual Result")).Returns("Order un-completed");

    var promptFlow = new PromptFlow(_readerMock.Object, _consoleMock.Object);

    TestDocument result = promptFlow.CollectTestDocument();

    Assert.Equal("Checkout", result.Title);
    Assert.Equal(2, result.Steps.Count);
    _consoleMock.VerifyAll();
    _readerMock.VerifyAll();
  }

  [Fact]
  public void CollectTestDocument_WithNoSteps_ThrowsValidationException()
  {
    _readerMock.Setup(x => x.ReadRequired("Title")).Returns(_fixture.Create<string>().Substring(0, 12));
    _readerMock.Setup(x => x.ReadRequired("Description")).Returns(_fixture.Create<string>().Substring(0, 12));
    _readerMock.Setup(x => x.ReadLine(It.IsAny<string>())).Returns(string.Empty);
    _readerMock.Setup(x => x.ReadRequired("Expected Result")).Returns(_fixture.Create<string>().Substring(0, 12));
    _readerMock.Setup(x => x.ReadRequired("Actual Result")).Returns(_fixture.Create<string>().Substring(0, 12));
    var promptFlow = new PromptFlow(_readerMock.Object, _consoleMock.Object);

    Assert.Throws<ValidationException>(() => promptFlow.CollectTestDocument());
  }
  // SOLVED: Add more tests (you can separate the tests)
  // Tests were added for FileExporter, PlainConsoleFormatter
}
