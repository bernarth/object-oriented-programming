# Inheritance vs Composition

## Inheritance

Inheritance is an "is a" relitionship. A subclass inherits fields and methods from a parent class (base class) and can add or override behaviors.

> Note: It promotes code reuse, but must be used carefully to avoid tight coupling.

## Composition

Composition is a strong relationship. It is a "has a" relationship type. One class contains another class as a field and delegates work to it.

> Note: It is more flexible and preferred in modern design because it promotes loose copling and better maintainability.

## When to use What

- Relationship

  - Inheritance: "is a"
  - Composition: "has a"

- Flexibility

  - Inheritance: Harder to change because of the hierarchy (useful when hierarchy is needed)
  - Composition: Easy to change behavior (Easier when depending on abstraction)

- Extensibility

  - Inheritance: Extend via subclassing
  - Composition: Extend via components (classes, methods)

- When to use:

  - Inheritance: When all derived classes share a strong **conceptual** relationship.
  - Composition: When you just need to reuse functionality without forcing a relationship.


## Key notes

- Composition works greatfully with Dependency Injection (DI), now standard in .NET 9 (Microsoft.Extensions.DepdendencyInjection)
- You can swap dependencies without changing the main classes (DI) (for example: INotifier => SlackNotifier, TeamsNotifier)
- Design Pattern 'Template Method' relyes on Inheritance.
- Design Pattern 'Strategy' relyes on Composition.