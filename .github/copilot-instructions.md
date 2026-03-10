<!-- GitHub Copilot instructions for this repository -->
# Copilot / Agent instructions — Claims solution

Purpose
-------
Provide quick, safe guidance for AI assistants working in this repository and surface useful commands, conventions, and suggested prompts.

Quick start (build & test)
--------------------------
Run these from the repository root:

```bash
dotnet restore
dotnet build Claims.sln -c Debug
dotnet test Claims.sln -c Debug
```

Notes
-----
- Some tests use Testcontainers and require a running container runtime (Docker Desktop or similar) for SQL Server / MongoDB emulation.
- Projects register services via `DependencyInjection.cs` files in the BusinessLogic and Database projects — prefer using those for DI registration.

Repository conventions (high level)
---------------------------------
- API layer: `API/Claims.API` — thin controllers that act as HTTP adapters. Keep business logic in the BusinessLogic layer.
- Business logic: `BusinessLogic/Claims.BusinessLogic` — services, interfaces, and strategy implementations live here.
- Database layer: `Database/Claims.Database` — repositories, EF Core context, and an async auditing pipeline (`Auditing/` + background service).
- Tests: each layer has a `.Tests` project next to it (e.g., `API/Claims.API.Tests`). Prefer running `dotnet test` at solution-level for CI.

What AI assistants should preserve
---------------------------------
- Preserve `DependencyInjection.cs` patterns and the background audit channel design; avoid moving heavy logic into controllers.
- Preserve existing strategy pattern implementations in `Services/Strategies` when refactoring premium calculations.

Suggested example prompts
-------------------------
- "List all public methods in `BusinessLogic/Claims.BusinessLogic/Services` and suggest any obvious API-surface reductions."
- "Add XML/OpenAPI annotations to `API/Claims.API/Controllers/ClaimsController.cs` for the Create and Get endpoints."
- "Search for blocking IO in `Database/Claims.Database/Services` and propose async replacements."
- "Run `dotnet build Claims.sln -c Debug` and resolve any build errors and warnings."


If something is unclear, ask for the preferred CI pipeline, DB access method for integration tests, and commit conventions.

--
Generated and maintained by repository agent bootstrap workflow.
