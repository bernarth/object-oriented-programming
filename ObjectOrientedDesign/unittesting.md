# Unit Testing Basics

## Definition

- Unit Testing is the process of testing the smallest testeable parts of your code that are isolated.
  - The purpose is to ensure that the logic of each part works correctly and in combination.

```csharp
public class Calculator
{
  public int Add(int a, int b) => a + b;

  public int Divide(int a, int b)
  {
    if (b == 0)
    {
      throw new DivideByZeroException("You cannot divide by zero!!!!!");
    }

    return a / b;
  }
}
```

> Note: In this course we are going to use XUnit for unit testing.

Parts of unit testing:

```csharp
public class CalculatorTests
{
  [Fact]
  public void Add_PositiveNumbers_ShouldReturnCorrectSum()
  {
    // Arrange: Initial setup before executing the method to test.
    var calculator = new Calculator();

    // Act: Execute the method under test
    var result = calculator.Add(2, 3);

    // Assert: Verify that the final result matches the expected result.
    Assert.Equals(5, result);
  }
}
```