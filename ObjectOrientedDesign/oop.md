# Object-Oriented Programming

## Definition

Pre-definition: Object-oriented means modeling that involves the practice of representing key concepts through objects. Depending on the problem, many concepts, people, places or things distinct each other. It is way of keeping things organized, flexible and reusable.

## Pillars
- Encalpsulation: Restrict data or bundle data or functions to expose certain data which can be accessed.
  - Access modifiers `public`, `protected`, `private`, etc.
- Abstraction: Hide data or is the idea of simplifying a concept in the problem domain to exposes only the necessary information.
  - The context is important
  - Rule of least Astonishment
- Inheritance: It allows a class (child) to inherit attributes and methods from another class (parent) promoting code reusability and hierarchy.
- Polymorphism: Many forms. It Allows an object to take multiple forms and behave differently based on the context.
  - Overloading (dynamic)
  - Overriding (static)

# Object-Oriented Design

## Definition

Take decision that involve abstract the reality and the context of the problem. Taking into account functional requirements and non-functional requirements.

- It works
  - Reusability
  - Flexibility
  - Maintainability
    - Secuirty
    - Code quality
    ...

## CRC - Class Responsibility Collaborator

For requirements you must identify: components, connections, responsibilities, when forming the conceptual design then go and create the classes.

### CRC Cards

They are used to record, organize and refine the components in your design.

- Responsibilities: The things that the entity will accomplish
- Collaborators: Other entities that the entity interacts with to fulfill its responsibilities.

# Example - Test Doc Cli

## Description

This project will generate diferent types of documents (console, markdown, html) based on the user input: title, description, steps, expected result and actual result (NTH, images)

## Actions

- The user will enter the required information
- The user will chose the output format
- The user will get the document

## Technical consideration

- `dotnet run --project TestDocCli -- --format md --file result` # md | html | console
- `Enter the Title:`
- ....
- `Document generated at:...`

## Version 1

- Won't have validations