# AI Prompts — curated examples

This file contains example prompts for common tasks and the expected outcome format to help AI assistants produce consistent, reviewable results.

1) Add XML/OpenAPI annotations to controller endpoints
- Prompt: "Add XML/OpenAPI annotations to the Create and Get endpoints in API/Claims.API/Controllers/ClaimsController.cs. Include `<summary>` and `[ProducesResponseType]` attributes for common responses."
- Expected outcome: updated `ClaimsController.cs` with XML `<summary>` above each action and `[ProducesResponseType]` attributes, plus a short PR description.

2) Write a unit test for a business service
- Prompt: "Create an NUnit test for `BusinessLogic/Claims.BusinessLogic/Services/CoversService.cs` that validates premium calculation for a bronze cover."
- Expected outcome: new test file in `BusinessLogic/Claims.BusinessLogic.Tests` with Arrange/Act/Assert and Testcontainers or mocks as needed.

3) Find blocking IO and propose async fixes
- Prompt: "Search `Database/Claims.Database/Services` for synchronous IO (e.g., file, DB calls) and propose async replacements with code snippets."
- Expected outcome: a short report listing files/lines and recommended async code changes.

4) Run tests and summarize failures
- Prompt: "Run the test suite and summarize failing tests with stack traces and likely root causes."
- Expected command to run locally:

```bash
dotnet test Claims.sln -c Debug
```

5) Small refactor request
- Prompt: "Refactor validation logic out of `API/Claims.API/Controllers/*` into a new `ClaimsValidator` in `BusinessLogic/` and update callers accordingly. Provide tests."
- Expected outcome: new validator class, updated DI registration, controller changes reduced to model-binding + validation call, and unit tests for the validator.

Writing useful prompts
----------------------
- Include the exact path(s) to edit, a short acceptance criterion, and whether tests are required.
- Provide example inputs and expected outputs when possible.
