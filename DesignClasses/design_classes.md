# Design classes

## Example - CLI tool (CLI - Command line interface)

As a user, I want a CLI that scaffolds a clean test case (name, purpose, steps) from reusable templates and exports it (console/markdown).

What the CLI does:

- The inforamtion for the test suites will be already available*
- Choose a template (e.g. "web-login", "api-health")
- Generate a TestCase with structured steps (actions/assertions)
- Optionally add the case to a TestSuite
- Export the result to console and/or Markdown file that you can paste into your board

What we need:

- TestCase
- ActionStep
- AssertStep
- TestSuite
- TestCaseDocument
- DocumentSection

- Template
- TemplateLibrary
- TestCaseBuilder

- ConsoleExporter
- MarkdownFileExporter

- CliRunner