# CompanyPost

## Project summary
CompanyPost is a modular ASP.NET Core Web API (targeting .NET 8, C# 12) that implements a document-post management system. It follows a layered architecture with explicit separation of concerns:

- **API (presentation)**: HTTP controllers exposing REST endpoints.
- **Application**: CQRS commands/queries, DTOs, and MediatR handlers implementing business use-cases.
- **Domain**: Entities, value objects, and domain interfaces.
- **Infrastructure**: EF Core DbContext, repositories, services (file, email, auth), migrations, and database seeding.

## Technical Overview

### Key technologies
- .NET 8 / C# 12
- ASP.NET Core Web API
- Entity Framework Core (MySQL) with snake_case naming convention
- MediatR for CQRS (commands / queries pattern)
- Dependency Injection and configuration via Microsoft.Extensions
- JWT Bearer authentication
- File upload handling via IFormFile

### High-level architecture
- Controllers create and send MediatR requests (commands/queries).
- Application layer contains DTOs and Request/Response models and Handler implementations that:
  - Validate domain constraints
  - Use repositories from IUnitOfWork
  - Save attachments via IFileService
  - Send notification emails using IEmailServices
  - Perform transactional operations via UnitOfWork
- Infrastructure contains EF Core DbContext (`CompanyPostDbContext`), repository implementations, authentication helpers, and concrete services.

### Project layout
- `API/CompanyPost.API` � controllers, program bootstrap, middleware (GlobalExceptionHandling).
- `Application/CompanyPosts.Application` � DTOs, CQRS Commands & Queries, Handlers, application-level usings.
- `Domain/CompanyPosts.Domain` � domain entities (e.g. `PostExternal`, `InComing`, `Contracts`, `PurchaseOrder`), enums, and interfaces like `IDocumentEntity`.
- `Infrastructure/CompanyPost.Infrastructure` � EF Core configuration, repositories, services (`FileService`, `EmailServices`), `SeedData`, and `InfrastructureServices` extension to wire services.

### Persistence and migrations
- Uses MySQL via EF Core with `ServerVersion.AutoDetect` and `UseSnakeCaseNamingConvention` configured in `InfrastructureServices`.
- `CompanyPostDbContext` defines DbSet for each aggregate (posts, contracts, users, publishers, worktypes, purchase orders, attachments, etc.).
- Migrations are included in `Infrastructure/CompanyPost.Infrastructure/Migrations` and `SeedData` seeds required reference data.
- Program startup calls `context.Database.Migrate()` and `SeedData.Initialize(context)` on startup.

### Patterns and conventions
- **CQRS + MediatR**: each use case has a request type (command/query) and a handler. Handlers coordinate repositories, services, and transactions.
- **Repository + Unit of Work pattern**: `IGenericRepository<T>`, `IUnitOfWork` used throughout application handlers.
- **Services** are injected via DI (e.g., `IFileService`, `IEmailServices`, `IJwTGenerator`).
- **Error handling**: `GlobalExceptionHandling` middleware centralizes exception responses.
- **Naming & style**: project intends to use repository coding standards and an `.editorconfig` / `CONTRIBUTING.md` � please follow those files (added to repo) when contributing.

### Authentication & Security
- JWT bearer authentication configured in `InfrastructureServices` using `JwtSettings` section from configuration.
- Tokens validated for issuer, audience, signing key, and lifetime.

### Important entities and flows
- **Documents**: `PostExternal`, `PostInternal`, `PostTransformer`, `InComing`, `Contracts`, `PurchaseOrder` (and their attachments). Entities share common fields via `PostBaseEntity` and `IDocumentEntity`.
- **Creating a document post typical flow**:
  1. Controller receives multipart/form-data with DTO (attachments as `IFormFile`).
  2. Controller sends a Create command through MediatR.
  3. Handler checks for duplicates (document number), computes `SerialNumber` using repository max helper, creates domain entity, starts a transaction, saves attachments (via `IFileService`), and persists entity through UnitOfWork.
  4. Handler optionally sends emails via `IEmailServices`.

### API surface (examples)
- **PostInternalController**: `POST /api/PostInternal/CreatePostInternal` (multipart/form-data)
- **PostExternalController**: `POST /api/PostExternal/CreatePostExternal`
- **PostTransformerController**: `POST /api/PostTransformer/CreatePostTransformer`
- **IncomingController**: `POST /api/Incoming/CreateIncoming`
- **ContractsController**: `POST /api/Contracts/create-contract`
- Several GET endpoints for retrieving max serial numbers, document numbers, and to-be-copied documents exist on each controller.

### Running locally
1. Ensure .NET 8 SDK is installed.
2. Configure `appsettings.json` or environment variables with a valid `DefaultConnection` (MySQL) and `JwtSettings` / `EmailSettings` as needed.
3. From the `API/CompanyPost.API` project root run `dotnet run` � Program will apply migrations and seed data automatically.
4. API will start and host endpoints (HTTPS redirection and static files configured).