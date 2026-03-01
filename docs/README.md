# Prerequisites & Used

- JetBrains Rider as IDE mostly for learning purposes.
- Will run the demo having a locally running instance.
- `Docker Desktop`

# Programming Task
Complete the backend for a multi-tier application for Insurance Claims Handling.
The use case is to maintain a list of insurance claims. The user should be able to create, delete and read claims.
## Task 1
The codebase is messy:
* The controller has too much responsibility. 
* Introduce proper layering within the codebase. 
* Documentation is missing.
* ++

Adhere to the SOLID principles.

### Task 2
As you can see, the API supports some basic REST operations. But validation is missing. The basic rules are:

* Claim
  * DamageCost cannot exceed 100.000
  * Created date must be within the period of the related Cover
* Cover
  * StartDate cannot be in the past
  * total insurance period cannot exceed 1 year

## Task 3
Auditing. The basics are there, but the execution of the DB command (INSERT & DELETE) blocks the processing of the HTTP request. How can this be improved? Look into some asynchronous patterns. It is ok to introduce an Azure managed service to help you with this (ServiceBus/EventGrid/Whatever), but that is not required. Whatever you can manage to get working which is in-memory is also ok.

## Task 4
One basic test is included, please add other (mandatory) unit tests. Note: If you start on this task first, you will find it hard to write proper tests. Some refactoring of the Claims API will be needed. 

## Task 5
Cover premium computation formula evolved over time. Fellow developers were lazy and omitted all tests. Now there are a couple of bugs. Can you fix them? Can you make the computation more readable?

Desired logic: 
* Premium depends on the type of the covered object and the length of the insurance period. 

## Implementation Summary

- **Task 1 — Layering & SOLID**: Introduced a clear 3-layer architecture.
  - Controllers: thin HTTP adapters in [API/Claims.API/Controllers](API/Claims.API/Controllers).
  - Business logic: services, validation and policies in [BusinessLogic/Claims.BusinessLogic/Services](BusinessLogic/Claims.BusinessLogic/Services).
  - Persistence/repositories: encapsulated in [Database/Claims.Database/Repositories](Database/Claims.Database/Repositories).

- **Task 2 — Validation**: Implemented business-level validation rules.
  - `DamageCost` cap and `Claim.Created` vs cover period: [BusinessLogic/Claims.BusinessLogic/Services/ClaimsService.cs](BusinessLogic/Claims.BusinessLogic/Services/ClaimsService.cs).
  - `Cover.StartDate` not in past, EndDate after StartDate, and max 1-year duration: [BusinessLogic/Claims.BusinessLogic/Services/CoversService.cs](BusinessLogic/Claims.BusinessLogic/Services/CoversService.cs).
  - Error messages centralised in: [BusinessLogic/Claims.BusinessLogic/Resources/ErrorMessages.resx](BusinessLogic/Claims.BusinessLogic/Resources/ErrorMessages.resx).

- **Task 3 — Auditing (non-blocking)**: Implemented an in-memory, asynchronous audit pipeline.
  - Producer (enqueue): `Database/Claims.Database/Services/AuditerService.cs` writes into `AuditChannel` (non-blocking enqueue).
  - Channel: `Database/Claims.Database/Auditing/AuditChannel.cs` (uses System.Threading.Channels).
  - Consumer: `Database/Claims.Database/Services/AuditBackgroundService.cs` processes messages in background and persists via `IAuditRepository`.
  - Persistence: `Database/Claims.Database/Auditing/AuditRepository.cs` uses EF Core `SaveChangesAsync()`.

- **Task 4 — Tests**: Added and extended unit tests.
  - Business logic tests: [BusinessLogic/Claims.BusinessLogic.Tests](BusinessLogic/Claims.BusinessLogic.Tests) (claims, covers, premium compute).
  - Database tests: [Database/Claims.Database.Tests](Database/Claims.Database.Tests) (audit channel, audit repository, db repository tests).
  - API tests: [API/Claims.API.Tests](API/Claims.API.Tests) (controller-level tests).

- **Task 5 — Premium computation**: Rewrote premium logic and provided strategies + tests.
  - Strategy-based calculation: [BusinessLogic/Claims.BusinessLogic/Services/PremiumComputeService.cs](BusinessLogic/Claims.BusinessLogic/Services/PremiumComputeService.cs).
  - Concrete strategies: [BusinessLogic/Claims.BusinessLogic/Services/Strategies](BusinessLogic/Claims.BusinessLogic/Services/Strategies) (Base, Yacht, PassengerShip, Tanker).
  - Unit tests verifying progressive discounts and multipliers: [BusinessLogic/Claims.BusinessLogic.Tests/PremiumComputeServiceTests.cs](BusinessLogic/Claims.BusinessLogic.Tests/PremiumComputeServiceTests.cs).
