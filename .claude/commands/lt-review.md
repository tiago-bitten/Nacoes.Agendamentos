# Seeds Code Review

Review the code I changed or the files I specify. Check every rule below and report violations. If everything is correct, say so briefly.

---

## Formatting

### Brackets are mandatory
`if`, `else`, `foreach`, `for`, `while`, `switch` — always use `{ }` brackets, even for single statements. Never put the body on the same line as the condition.

**Wrong:**
```csharp
if (x) return y;
if (x) { return y; }
foreach (var i in list) DoSomething(i);
```

**Correct:**
```csharp
if (x)
{
    return y;
}

foreach (var i in list)
{
    DoSomething(i);
}
```

### Indentation and spacing
- 4 spaces, no tabs.
- One blank line between logical sections. No excessive blank lines.

### Line breaking and vertical alignment
When a line exceeds the editor's vertical guide (roughly 120 characters) or has multiple parameters/arguments, break it so that each item sits on its own line, vertically aligned. This applies to: record declarations, method calls, object initializers, constructor arguments, and LINQ chains.

**Wrong:**
```csharp
private sealed record Request(Guid ProductId, string BatchNumber, decimal Quantity, decimal UnitCost, Instant EnteredAt, Instant? ManufacturedAt, Instant? ExpiresAt, Guid? StorageLocationId);

var command = new CreateBatchCommand(request.ProductId, request.BatchNumber, request.Quantity, request.UnitCost, request.EnteredAt, request.ManufacturedAt, request.ExpiresAt, request.StorageLocationId);
```

**Correct:**
```csharp
private sealed record Request(
    Guid ProductId,
    string BatchNumber,
    decimal Quantity,
    decimal UnitCost,
    Instant EnteredAt,
    Instant? ManufacturedAt,
    Instant? ExpiresAt,
    Guid? StorageLocationId);

var command = new CreateBatchCommand(
    request.ProductId,
    request.BatchNumber,
    request.Quantity,
    request.UnitCost,
    request.EnteredAt,
    request.ManufacturedAt,
    request.ExpiresAt,
    request.StorageLocationId);
```

Key rules:
- The opening parenthesis stays on the same line as the declaration/call.
- Each parameter/argument goes on its own line, indented one level (4 spaces).
- The closing parenthesis (or `);`) sits at the end of the last parameter line.
- This also applies to chained method calls (`.Where(...)`, `.Select(...)`, etc.) — each chained call on its own line, indented.

---

## Result Pattern

### Destructure Result values
After checking `IsFailure`, extract `.Value` into a named variable. Do NOT use `.Value` inline repeatedly.

**Wrong:**
```csharp
var batchResult = Batch.Create(...);
if (batchResult.IsFailure) return batchResult.Error;
await context.Batches.AddAsync(batchResult.Value, ct);
return new Response(batchResult.Value.Id, batchResult.Value.Name);
```

**Correct:**
```csharp
var batchResult = Batch.Create(...);
if (batchResult.IsFailure)
{
    return batchResult.Error;
}

var batch = batchResult.Value;
await context.Batches.AddAsync(batch, ct);
return new Response(batch.Id, batch.Name);
```

### Never throw exceptions for business logic
Use `Result<T>` / `Result` for all expected failures. Domain Create methods return `Result<T>`. Handlers return `Result` or `Result<T>`.

### Error definitions
Errors go in static classes named `[Entity]Errors` with `static readonly Error` fields. Code format: `"entity.errorname"` (lowercase, dot-separated). Use the correct error type: `Error.Validation`, `Error.NotFound`, `Error.Conflict`, `Error.Problem`, `Error.Forbidden`.

---

## Command Handlers

### Class declaration
- `internal sealed class` — never `public`.
- Primary constructor for dependency injection.
- Implement `ICommandHandler<TCommand>` or `ICommandHandler<TCommand, TResponse>`.

```csharp
internal sealed class CreateBatchHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<CreateBatchCommand, CreateBatchResponse>
```

### Method signature
- Always `HandleAsync`, return `Task<Result>` or `Task<Result<T>>`.
- CancellationToken parameter always named `ct`.
- Pass `ct` to every async call (`SaveChangesAsync`, `FirstOrDefaultAsync`, `AnyAsync`, `AddAsync`, etc.).

### SaveChangesAsync usage
- Minimize calls to `SaveChangesAsync`. Batch all entity additions/modifications and call once at the end when possible.
- Multiple `SaveChangesAsync` calls are only acceptable when you need a database-generated ID before the next operation (e.g., StockMovement ID needed for BatchMovement).

### Side effects via Domain Events
- Command handlers focus on the primary operation.
- Side effects (audit logs, secondary entity creation) should be handled by Domain Event Handlers, not inline in the command handler.
- Domain events are raised by entity `Create` methods via `Raise(new SomeDomainEvent(...))`.

---

## Query Handlers

- `internal sealed class`, primary constructor.
- Implement `IQueryHandler<TQuery, TResponse>`.
- Use `AsNoTracking()` for read-only queries.
- Use `.Select()` projections to DTOs — avoid returning full entities.

### Query response pattern
Query handlers must NEVER return `List<Dto>` directly. Always use a dedicated `[Action][Entity]Response` record that inherits from `QueryResponseBase`.

- The response record inherits from `QueryResponseBase` (provides `NextCursor` and `Total` for cursor pagination).
- List items go in a **nested record** inside the response, exposed via an `Items` property.
- Both the response and the nested item record use **property declaration** (not positional parameters) to support EF Core `.Select()` projection with object initializers.

**Wrong:**
```csharp
public sealed record GetAllBatchesQuery(string? Cursor = null) : IQuery<List<BatchDto>>;

internal sealed class GetAllBatchesHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllBatchesQuery, List<BatchDto>>
```

**Correct:**
```csharp
// Query — inherits from PagedQuery (provides Cursor + PageSize)
public sealed record GetAllBatchesQuery(
    string? Cursor = null,
    int PageSize = 20)
    : PagedQuery<GetAllBatchesResponse>(Cursor, PageSize);

// Response
public sealed record GetAllBatchesResponse : QueryResponseBase
{
    public required List<GetAllBatchesResponse.Item> Items { get; init; }

    public sealed record Item
    {
        public required Guid Id { get; init; }
        public required string ProductName { get; init; }
        public required string BatchNumber { get; init; }
        // ... other properties with required + { get; init; }
    }
}

// Handler
internal sealed class GetAllBatchesHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllBatchesQuery, GetAllBatchesResponse>
{
    public async Task<Result<GetAllBatchesResponse>> HandleAsync(
        GetAllBatchesQuery query, CancellationToken ct)
    {
        var q = context.Batches
            .AsNoTracking()
            .AsQueryable();

        var (batches, total, nextCursor) = await q.ToPagedResponseAsync(query.Cursor, query.PageSize, ct);

        var items = batches.Select(b => new GetAllBatchesResponse.Item
        {
            Id = b.Id,
            BatchNumber = b.BatchNumber,
        }).ToList();

        return new GetAllBatchesResponse
        {
            Items = items,
            Total = total,
            NextCursor = nextCursor,
        };
    }
}
```

### QueryResponseBase
All list-returning query responses must inherit from `QueryResponseBase`:

```csharp
public abstract record QueryResponseBase
{
    public string? NextCursor { get; init; }
    public required int Total { get; init; }
}
```

If this base class does not exist yet, it must be created at `Application/Shared/Messaging/QueryResponseBase.cs`.

### Cursor pagination is mandatory for list queries
Query handlers that return a list of items (GetAll, GetPending, GetByProduct, etc.) **must** use `ToPagedResponseAsync` for pagination. Never use `.ToListAsync()` directly to return unbounded lists.

**Wrong:**
```csharp
public sealed record GetAllProductsQuery(string? Cursor = null) : IQuery<GetAllProductsResponse>;

var items = await q.OrderBy(p => p.Name)
    .Select(p => new Response.Item { ... })
    .ToListAsync(ct);

return new Response { Items = items, Total = items.Count };
```

**Correct:**
```csharp
public sealed record GetAllProductsQuery(
    string? Cursor = null,
    int PageSize = 20)
    : PagedQuery<GetAllProductsResponse>(Cursor, PageSize);

var (entities, total, nextCursor) = await q.ToPagedResponseAsync(query.Cursor, query.PageSize, ct);

var items = entities.Select(p => new Response.Item { ... }).ToList();

return new Response
{
    Items = items,
    Total = total,
    NextCursor = nextCursor,
};
```

Key rules:
- The query record must inherit from `PagedQuery<TResponse>` — never implement `IQuery<TResponse>` directly for list queries. `PagedQuery` provides `Cursor` and `PageSize`.
- `ToPagedResponseAsync` operates on `IQueryable<Entity>` (before `.Select()`), projection happens in-memory.
- Handlers must use `query.PageSize` — never hardcode the page size (e.g., `20`) in `ToPagedResponseAsync` calls.
- The endpoint must bind the `Cursor` and `PageSize` query parameters (via `[AsParameters]` or `[FromQuery]`).

---

## Domain Entities

### Factory method pattern
- Private parameterless constructor: `private Entity() { }`
- Static `Create` method returning `Result<T>`.
- Validate inputs first, return errors from `[Entity]Errors`.
- Use object initializer: `new Entity { Prop = value }`.
- Trim string inputs.
- Raise domain events at the end.

### Entity base classes
- `TenantEntity` for tenant-scoped entities (has `TenantId`).
- `RemovableTenantEntity` for soft-deletable entities.
- Properties use `private set` or `protected init`.

### No logic in constructors
All creation logic goes in the static `Create` method.

---

## Commands, Queries, Responses

- Always `public sealed record`.
- Commands implement `ICommand` or `ICommand<TResponse>`.
- Queries implement `IQuery<TResponse>`.
- **List-returning queries** (GetAll, GetPending, GetByProduct, etc.) must inherit from `PagedQuery<TResponse>` instead of implementing `IQuery<TResponse>` directly. `PagedQuery` centralizes `Cursor` and `PageSize` parameters.
- Response class named `[Action][Entity]Response` (e.g., `CreateBatchResponse`, `GetAllBatchesResponse`).
- Query list responses inherit from `QueryResponseBase` and use a nested `Item` record with property declaration (see Query Handlers section).
- Never use `List<Dto>` as the query response type — always wrap in a dedicated response record.

---

## Domain Events

- `public sealed record` inheriting from `DomainEvent`.
- Named `[Entity][Action]DomainEvent` (e.g., `BatchCreatedDomainEvent`).
- Event handlers are `internal sealed class` implementing `IDomainEventHandler<T>`.
- Run in a separate transaction after the command handler commits.

---

## Endpoints

### No `api/` prefix in routes
Endpoint routes must NOT include the `api/` prefix — the URL is implicitly an API URL. This applies to both route definitions and `Results.Created` location headers.

**Wrong:**
```csharp
app.MapGet("api/products", async (...) => { });
return Results.Created($"/api/products/{r.ProductId}", r);
```

**Correct:**
```csharp
app.MapGet("products", async (...) => { });
return Results.Created($"/products/{r.ProductId}", r);
```

### Class declaration
- `internal sealed class` — never `public`.
- Implement `IEndpoint`.

### Class naming
The endpoint class name must be the **action only** (e.g., `GetAll`, `Create`, `Cancel`). Do NOT include the entity/feature name — it is already expressed by the namespace/folder.

**Wrong:**
```csharp
// File: Endpoints/Nfe/CancelNfeFetch.cs
namespace Seeds.API.Endpoints.Nfe;
internal sealed class CancelNfeFetch : IEndpoint  // ❌ "NfeFetch" is redundant
```

**Correct:**
```csharp
// File: Endpoints/Nfe/Cancel.cs
namespace Seeds.API.Endpoints.Nfe;
internal sealed class Cancel : IEndpoint  // ✅ action only
```

```csharp
// File: Endpoints/Batches/GetAll.cs
namespace Seeds.API.Endpoints.Batches;
internal sealed class GetAll : IEndpoint  // ✅ action only
```

---

## Naming

| Element | Convention | Example |
|---------|-----------|---------|
| Classes/Methods | PascalCase | `CreateBatchHandler` |
| Private fields | `_camelCase` | `_domainEvents` |
| Parameters/locals | camelCase | `productId`, `ct` |
| Interfaces | `I` prefix | `IUserContext` |
| Errors class | `[Entity]Errors` | `BatchErrors` |
| Commands | `[Action][Entity]Command` | `CreateBatchCommand` |
| Domain Events | `[Entity][Action]DomainEvent` | `BatchCreatedDomainEvent` |

---

## File Structure and Namespaces

- File-scoped namespaces: `namespace Seeds.Application.Commands.Batches.Create;`
- `using` statements right after namespace, no blank line between.
- One class per file (except small related types like Command + Response in the same file).

---

## Types and Libraries

- **NodaTime** — Use `Instant` for all datetime fields, never `DateTime`.
- **DbContext** — Access via `IApplicationDbContext`, never inject `DbContext` directly.
- **User context** — Access via `IUserContext` for `TenantId`, `UserId`, `Role`, etc.

---

## No Comments in Code

Code must be self-explanatory. Do NOT use comments to explain what the code does — if it's not clear, refactor until it is (better names, extract method, simplify logic).

Comments are only acceptable for truly exceptional cases where external context is required and no amount of refactoring can make the intent obvious (e.g., a workaround for a third-party bug, a non-obvious regulatory rule).

If a comment exists and the code could be made clearer through renaming, extracting, or restructuring, report a violation.

**Wrong:**
```csharp
// Check if the batch is expired
if (batch.ExpirationDate < SystemClock.Instance.GetCurrentInstant())
{
    return BatchErrors.Expired;
}

// Calculate the total price with discount
var total = items.Sum(i => i.Price * i.Quantity) * (1 - discount);
```

**Correct:**
```csharp
if (batch.IsExpired())
{
    return BatchErrors.Expired;
}

var total = CalculateTotalWithDiscount(items, discount);
```

---

## Consistency Checks

### Entity changes require migration check
If a domain entity was modified (new property, property type change, property removal, or MaxLength constant change that affects the database schema), verify that an EF Core migration was created or flagged as needed. Report a violation if a schema-affecting entity change has no corresponding migration.

### Every Command must have a CommandValidator
Every `ICommand` / `ICommand<TResponse>` must have a corresponding `AbstractValidator<TCommand>` in the same folder. If a Command exists without a Validator, report it as a violation.

### List-returning queries must inherit from PagedQuery
Any query that returns a list of items (GetAll, GetPending, GetByProduct, etc.) must inherit from `PagedQuery<TResponse>` — not `IQuery<TResponse>` directly. If a list-returning query implements `IQuery<TResponse>` instead of extending `PagedQuery<TResponse>`, report it as a violation.

### MaxLength constants must be centralized in entities
If a `HasMaxLength(N)` or `MaximumLength(N)` uses a hardcoded number instead of a `public const int` from the domain entity, report it as a violation. MaxLength values must be defined as `public const int` in the domain entity and referenced in:
- EF Core Configuration (`HasMaxLength(Entity.ConstName)`)
- FluentValidation Validator (`MaximumLength(Entity.ConstName)`)

**Wrong:**
```csharp
// Configuration
builder.Property(p => p.Name).HasMaxLength(300);
// Validator
RuleFor(x => x.Name).MaximumLength(300);
```

**Correct:**
```csharp
// Entity
public const int NameMaxLength = 300;
// Configuration
builder.Property(p => p.Name).HasMaxLength(Product.NameMaxLength);
// Validator
RuleFor(x => x.Name).MaximumLength(Product.NameMaxLength);
```

---

## Review output

For each violation found, report:
1. File and line
2. Rule violated
3. What it should look like

If no violations, say "No violations found."
