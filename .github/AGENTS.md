# AGENTS

Purpose
-------
Describe recommended agent roles and `applyTo` patterns for automated assistants working in this repository.

Agent roles
-----------
- **code-reviewer** — Focus: PR-level feedback, small refactors, API surface. Apply to: `BusinessLogic/**`, `Database/**`, `API/**`.
- **test-writer** — Focus: add unit and integration tests, mocking, Testcontainers setup. Apply to: `**/*.Tests/**`.
- **migrator** — Focus: EF Core migrations, DB schema changes, data migrations. Apply to: `Database/**`, `Migrations/**`.
- **doc-writer** — Focus: README, docs, prompts, and agent instructions. Apply to: `docs/**`, `.github/**`.

Guidelines for all agents
------------------------
- Preserve architectural boundaries: controllers in `API/` must remain thin; business rules stay in `BusinessLogic/`.
- BusinessLogic `BusinessLogic/` is the core and has no dependencies.
  - Provides Interfaces for the other layers `API/` and `Database/`.
  - Other layers do not know each other.
- Use existing `DependencyInjection.cs` files for service registration; avoid ad-hoc DI additions.
- Run tests locally via `dotnet test` before proposing changes that touch behavior.
- Do not commit database credentials or secrets; use environment variables and secrets management.

`applyTo`
------------------
- code-reviewer: `applyTo: ["BusinessLogic/**", "API/**", "Database/**"]`
- test-writer: `applyTo: ["**/*.Tests/**"]`
- migrator: `applyTo: ["Database/**"]`

When in doubt
-------------
Open a short discussion or PR describing the change and include failing tests or a migration rollback plan.
