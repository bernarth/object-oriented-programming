# TestDocCli - AI Coding Agent Instructions

## Project Overview
TestDocCli is a .NET 9 console application that generates test documentation in multiple formats (console, HTML, markdown) based on user input. This is an educational OOP project demonstrating clean architecture principles with dependency injection.

## Architecture Patterns

### Dependency Injection Setup
- Uses Microsoft.Extensions.Hosting with `HostApplicationBuilder` in `Program.cs`
- All services registered as singletons using interfaces
- Command-line configuration bound to `AppSettings` class
- Entry point is `AppHostedService` which orchestrates the main workflow

### Key Service Boundaries
- **AppCore**: Business logic and orchestration (`PromptFlow`, `FileExporter`, `AppHostedService`)
- **Formatters**: Output format implementations (`IOutputFormatter` with HTML, Markdown, Console variants)
- **InputOutput**: Console abstraction layer (`IConsole`, `IInputReader` for testability)
- **Model**: Single domain entity `TestDocument` with required init properties

### Factory Pattern Implementation
`OutputFormatterFactory` uses string-based format selection:
```csharp
"md" or "markdown" => new MarkdownFormatter()
"html" => new HtmlFormatter()
"console" or "text" or "" => new PlainConsoleFormatter() // default
```

## Development Workflows

### Building & Running
```bash
dotnet build
dotnet run -- --Format=html --OutputDirectory=./output
```

### Command-Line Options
- `--Format`: Output format (console/text, md/markdown, html)  
- `--OutputDirectory`: Where to save files (defaults to current directory)

## Project-Specific Conventions

### File Naming Pattern
All exported files use timestamp-based naming: `{baseNameHint}-{yyyyMMdd-HHmmss}.{extension}`

### Interface Segregation
- `IConsole` for basic write/read operations
- `IInputReader` for complex input validation (separate from console operations)
- `IOutputFormatter` defines both `FileExtension` property and `Format()` method

### TODO Pattern
Code contains educational TODs marking future enhancements:
- Title normalization for filenames
- Format argument validation with user messages
- Diagram generation reminder

### Error Handling
Minimal error handling - uses nullable reference types and argument validation in constructors. Primary constructor pattern used throughout for DI.

## Key Files to Understand
- `Program.cs`: DI container setup and service registration
- `AppHostedService.cs`: Main application flow orchestration  
- `PromptFlow.cs`: User input collection logic with step-by-step prompting
- `OutputFormatterFactory.cs`: Format selection logic
- `TestDocument.cs`: Core domain model with required init properties

When adding new formatters, implement `IOutputFormatter` and update the factory's switch expression.