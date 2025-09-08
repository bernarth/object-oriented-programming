# Design classes

## Test-automation Mini-framework 

This system demonstrates pieces that we can reuse to automate testing cases.

What we are going to build are the following steps:

- `TestStep` - a simple action like "Click login button" or "Send GET `/health`"

- `TestCase` (base) - runnable test with a name and steps.

- `WebTestCase` & `ApiTestCase` - specialization that add tiny, data (`Url`, `Endpoint`)

- `TestSuite` - a collection of test cases you run together (example: `Smoke Suite`)

- `IReporter` - an interface for outputting results (HTML, JSON, XML, etc)

- `Report` & `ReportSection` - the final document of a run.

- `Logger` - simple logging service

- `TestRunner` - orchestrates running a suite.

- `Program` - wires everything so you can run and demo each test suite.