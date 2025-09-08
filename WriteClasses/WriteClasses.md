# Write classes

## Definition

- A class is a blueprint that defines the structure and behavior that objects created from it will have.

## Real-world Example

Imagine we want to represent a Test Case in an automation framework:

Caracteristics:

- A test case has attributes like Id, Title, Priority, Priority, FailureReason.
- Id: This should be automatically generated.
- Title: We can modify the title but, we are going to validate the title. 
  - Max length 120
  - We need to expose the limit
  - The title should not be null or white space
- Priority: The priority should have an initial priority.
  - We can change the priority after the test case was created.
  - The priority can be P1, P2, P3, P4. Where P1 is the highest priority.
- Status: The status can be NotRun, Passed or Failure.
- FailureReason: This can be null or 150 max length

Behavior:

- We need a way to know if the test case is high priority.
- ....
- The exuction of the test should be dynamic. This means we can add any behavior in the testcase.

## When creating a class...

...it is important to remember that we need to defined depending on the context.
...since the context is important, most of the time diagrams can save us time when looking at a class.

# Notes

- We need to avoid the magic values (commonly numbers or strings)
- We can have two constructors as well:

```csharp
public TestCase(string title)
{
  _title = title;
  Priority = DefaultPriority;
}

public TestCase(string title, string priority)
{
  _title = title;
  Priority = priority;
}
```

- We can have as many constructors as needed
```csharp
public class ApiClient
{
  public ApiClient() {}
  public ApiClient(string baseUrl) {}
  public ApiClient(string baseUrl, int timeout) {}
  public ApiClient(string baseUrl, int timeout, string key) {}
}
```

# Access modifiers

## Definition

Access modifiers in C# are **keywords** that define the **visibility** and **accessibility** of classes, methods, variables, and other members

> In general they define the visbility and accessibility.

They are important because:

- Protect data and prevent accidental misuse.
- Improve maintainability and security by controlling who can access what.
- Define clear boundaries between the different parts of the solution.
- [ Enforce encapsulation; part of the OOP pillars ]

## Notes:

- Default values:
  - If you don't specify, class members are `private`
  - Top-level classes are internal

- Best practice: Start with the most restrictive modifier (`private`)

# Coupling and Cohesion

## Cohesion

- Refers to how focused and unified a class is on doing one well-defined job.
- A highly cohesive class has methods and attributes that are directly related to each other and to the class's purpose (context)
- Low cohesion means the class tries to do too many unrelated things. Exmple: God Class

## Coupling

- Refers to how much a class depends on other class (methods as well)
- Tight coupling: A class knows too much about the details of other classes, this means it might be hard to maintain or test.
- Loose coupling: A class only knows what it needs to know (we can apply with: interfaces, abstractions, or clean code in general). This makes the code easier to modify, test, or replace without breaking other classes.

Good design = High Cohesion + Low Coupling

## Example

Imagine you are writting an automation framework for testing a web application.

- A high cohesive class could be a LoginPage class that only deals with login functionality: entering user, entering password, clicking login.

- Loose coupling: Your LoginPage depends only on a WebDriver interface to interact with the browser.

## Notes

- Your classes, methods and members in general should be open for extension but closed for modification.
- If you notice that you are duplicating your code it might mean that you have a high coupling.
