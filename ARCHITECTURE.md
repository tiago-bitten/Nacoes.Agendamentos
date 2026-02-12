# Arquitetura Hexagonal - Guia de Referencia (FotoTaker)

Documento detalhado da arquitetura hexagonal (Ports & Adapters) implementada no projeto FotoTaker.
Use este guia como referencia para migrar outros projetos para a mesma arquitetura.

---

## Indice

1. [Visao Geral da Arquitetura](#1-visao-geral-da-arquitetura)
2. [Estrutura de Pastas Completa](#2-estrutura-de-pastas-completa)
3. [Camada Domain (Core)](#3-camada-domain-core)
4. [Camada Application (Core)](#4-camada-application-core)
5. [Camada Infrastructure](#5-camada-infrastructure)
6. [Driving Adapters (Adaptadores de Entrada)](#6-driving-adapters-adaptadores-de-entrada)
7. [Driven Adapters (Adaptadores de Saida)](#7-driven-adapters-adaptadores-de-saida)
8. [Fluxo de Dependencias](#8-fluxo-de-dependencias)
9. [Design Patterns Utilizados](#9-design-patterns-utilizados)
10. [CQRS - Commands e Queries](#10-cqrs---commands-e-queries)
11. [Decorator Pattern (Cross-Cutting Concerns)](#11-decorator-pattern-cross-cutting-concerns)
12. [Result Pattern (Railway-Oriented Programming)](#12-result-pattern-railway-oriented-programming)
13. [Strategy e Factory Patterns](#13-strategy-e-factory-patterns)
14. [Registro de Dependencias (DI)](#14-registro-de-dependencias-di)
15. [Endpoints (Minimal APIs)](#15-endpoints-minimal-apis)
16. [Configuracoes e Docker](#16-configuracoes-e-docker)
17. [Guia Passo a Passo para Migrar outro Projeto](#17-guia-passo-a-passo-para-migrar-outro-projeto)

---

## 1. Visao Geral da Arquitetura

A arquitetura hexagonal (tambem chamada de **Ports & Adapters**) isola completamente a logica de negocio (dominio) dos detalhes tecnicos (banco de dados, APIs externas, filas de mensagens, etc).

### Principio Central

O **Core** (Domain + Application) nao conhece nenhuma tecnologia externa. Ele define **Ports** (interfaces) que os **Adapters** implementam.

### Diagrama de Alto Nivel

```
                    ┌─────────────────────────────┐
                    │      MUNDO EXTERNO           │
                    │  (HTTP, Browser, CLI, etc)   │
                    └─────────────┬───────────────┘
                                  │
                    ┌─────────────▼───────────────┐
                    │     DRIVING ADAPTERS         │
                    │   (Adaptadores de Entrada)   │
                    │                              │
                    │  API (Minimal API Endpoints) │
                    │  Web (Razor Pages)           │
                    └─────────────┬───────────────┘
                                  │
                                  │  usa interfaces (Ports)
                                  │
              ┌───────────────────▼────────────────────┐
              │            CORE (Hexagono)              │
              │                                         │
              │  ┌───────────────────────────────────┐  │
              │  │         APPLICATION                │  │
              │  │  Commands / Queries / Handlers     │  │
              │  │  Ports (interfaces para fora)      │  │
              │  │  Behaviors (Decorators)             │  │
              │  └───────────────┬───────────────────┘  │
              │                  │                       │
              │  ┌───────────────▼───────────────────┐  │
              │  │           DOMAIN                   │  │
              │  │  Entities / Value Objects          │  │
              │  │  Aggregates / Domain Events        │  │
              │  │  Specifications / Errors           │  │
              │  └───────────────────────────────────┘  │
              └───────────────────┬────────────────────┘
                                  │
                                  │  implementa interfaces (Ports)
                                  │
                    ┌─────────────▼───────────────┐
                    │      DRIVEN ADAPTERS         │
                    │  (Adaptadores de Saida)       │
                    │                              │
                    │  Postgres (EF Core)           │
                    │  MinIO (Object Storage)       │
                    │  RabbitMQ (Mensageria)        │
                    └─────────────┬───────────────┘
                                  │
                    ┌─────────────▼───────────────┐
                    │     MUNDO EXTERNO            │
                    │  (DB, Storage, Filas, etc)   │
                    └─────────────────────────────┘
```

### Regra de Ouro

```
Domain  -->  NAO depende de NADA (zero referencias externas)
Application  -->  Depende APENAS do Domain
Infrastructure  -->  Depende de Application e Domain
Adapters  -->  Implementam os Ports definidos no Application
```

---

## 2. Estrutura de Pastas Completa

```
src/
├── Core/                                   # O "hexagono" - logica pura
│   ├── Domain/                             # Camada mais interna
│   │   ├── Domain.csproj                   # net10.0 - ZERO dependencias externas
│   │   ├── Users/                          # Aggregate Root: User
│   │   │   ├── User.cs                     # Entidade principal
│   │   │   ├── UserStatus.cs               # Enum (Active, Blocked)
│   │   │   ├── UserErrors.cs               # Erros especificos do dominio
│   │   │   ├── Logins/                     # Sub-entidade
│   │   │   │   ├── UserLogin.cs            # Entidade de login
│   │   │   │   ├── UserLoginProvider.cs     # Enum (Local, Google, GitHub, Facebook)
│   │   │   │   └── UserLoginErrors.cs
│   │   │   └── Specifications/             # Query Specifications
│   │   │       └── UserByEmailSpec.cs
│   │   ├── Images/                         # Aggregate Root: Image
│   │   │   ├── Image.cs                    # Entidade principal
│   │   │   ├── ImageErrors.cs
│   │   │   ├── Links/                      # Sub-entidade
│   │   │   │   ├── ImageLink.cs
│   │   │   │   └── ImageLinkProvider.cs     # Enum (MinIO, extensivel)
│   │   │   └── Views/                      # Sub-entidade
│   │   │       └── View.cs
│   │   ├── RefreshTokens/                  # Entidade independente
│   │   │   ├── RefreshToken.cs
│   │   │   ├── RefreshTokenStatus.cs
│   │   │   └── RefreshTokenErrors.cs
│   │   └── Shared/                         # Building blocks do DDD
│   │       ├── Entities/
│   │       │   ├── Entity.cs               # Classe base (Id, CreatedAt, Events)
│   │       │   └── RemovableEntity.cs      # Soft delete (IsRemoved)
│   │       ├── Results/
│   │       │   ├── Result.cs               # Result<T> pattern
│   │       │   ├── Error.cs                # Error record (Code, Description, Type)
│   │       │   ├── ErrorType.cs            # Enum (Failure, Validation, NotFound, etc)
│   │       │   └── ValidationError.cs
│   │       ├── Events/
│   │       │   ├── IEvent.cs               # Marker interface para domain events
│   │       │   └── IEventHandler.cs        # Handler generico
│   │       ├── Specifications/
│   │       │   └── ISpecification.cs       # Specification pattern
│   │       ├── ValueObjects/
│   │       │   ├── Email.cs                # Value Object com validacao
│   │       │   └── Phone.cs                # Value Object com validacao
│   │       └── Errors/
│   │           └── RemovableEntityErrors.cs
│   │
│   └── Application/                        # Casos de uso
│       ├── Application.csproj              # net10.0 - depende apenas do Domain
│       ├── DependencyInjection.cs          # Registro de handlers e validators
│       ├── Commands/                       # Operacoes de escrita (CQRS)
│       │   ├── Auth/
│       │   │   ├── Login/
│       │   │   │   ├── LoginCommand.cs
│       │   │   │   ├── LoginHandler.cs
│       │   │   │   ├── LoginResponse.cs
│       │   │   │   └── LoginValidator.cs
│       │   │   └── RefreshToken/
│       │   │       ├── RefreshTokenCommand.cs
│       │   │       ├── RefreshTokenHandler.cs
│       │   │       └── RefreshTokenResponse.cs
│       │   ├── Images/
│       │   │   └── Create/
│       │   │       ├── CreateImageCommand.cs
│       │   │       ├── CreateImageHandler.cs
│       │   │       ├── CreateImageResponse.cs
│       │   │       └── CreateImageValidator.cs
│       │   └── Users/
│       │       └── Create/
│       │           ├── CreateUserCommand.cs
│       │           ├── CreateUserHandler.cs
│       │           ├── CreateUserResponse.cs
│       │           └── CreateUserValidator.cs
│       ├── Queries/                        # Operacoes de leitura (CQRS)
│       │   ├── Images/
│       │   │   └── GetByPath/
│       │   │       ├── GetImageByPathQuery.cs
│       │   │       ├── GetImageByPathHandler.cs
│       │   │       └── GetImageByPathResponse.cs
│       │   └── Users/
│       │       └── GetById/
│       │           ├── GetUserByIdQuery.cs
│       │           ├── GetUserByIdHandler.cs
│       │           └── GetUserByIdResponse.cs
│       ├── Services/                       # Ports para servicos de infra
│       │   ├── IPasswordHasher.cs
│       │   └── ITokenGenerator.cs
│       ├── Strategies/                     # Ports para strategies
│       │   ├── IImageLinkStrategy.cs
│       │   ├── IImageLinkStrategyFactory.cs
│       │   └── Login/
│       │       ├── ILoginStrategy.cs
│       │       └── ILoginStrategyFactory.cs
│       └── Shared/
│           ├── Behaviors/                  # Decorators (cross-cutting)
│           │   ├── ValidationDecorator.cs
│           │   ├── TransactionDecorator.cs
│           │   └── LoggingDecorator.cs
│           ├── Contexts/                   # Ports para contextos
│           │   ├── IApplicationDbContext.cs # Abstrai o banco de dados
│           │   └── IUserContext.cs          # Abstrai o usuario logado
│           ├── Events/
│           │   └── ApplicationEvents.cs
│           ├── Extensions/
│           │   └── QueryableExtensions.cs
│           ├── Managers/
│           │   └── IApplicationLinkManager.cs
│           ├── Messaging/                  # Interfaces do CQRS
│           │   ├── ICommand.cs
│           │   ├── ICommandHandler.cs
│           │   ├── IQuery.cs
│           │   └── IQueryHandler.cs
│           └── Ports/                      # Ports genericos
│               ├── IEventsDispatcher.cs
│               └── IMessagePublisher.cs
│
├── Adapters/                               # Implementacoes dos Ports
│   ├── Driving/                            # ENTRADA (quem chama o Core)
│   │   ├── API/                            # REST API (Minimal APIs)
│   │   │   ├── API.csproj                  # net10.0 web - Ponto de entrada
│   │   │   ├── Program.cs                  # Composicao e bootstrap
│   │   │   ├── appsettings.json
│   │   │   ├── appsettings.Development.json
│   │   │   ├── Endpoints/                  # Endpoints agrupados por dominio
│   │   │   │   ├── IEndpoint.cs            # Interface base
│   │   │   │   ├── Tags.cs                 # Constantes de agrupamento (Swagger)
│   │   │   │   ├── Auth/
│   │   │   │   │   ├── Login.cs
│   │   │   │   │   └── RefreshToken.cs
│   │   │   │   ├── Images/
│   │   │   │   │   └── Create.cs
│   │   │   │   └── Users/
│   │   │   │       ├── Create.cs
│   │   │   │       └── GetById.cs
│   │   │   ├── Extensions/
│   │   │   │   ├── EndpointExtensions.cs    # Auto-discovery de endpoints
│   │   │   │   ├── ResultExtensions.cs      # Match pattern para Result<T>
│   │   │   │   └── AuthorizationEndpointBuilderExtensions.cs
│   │   │   └── Infra/
│   │   │       └── CustomResults.cs         # ProblemDetails formatting
│   │   │
│   │   └── Web/                            # Razor Pages (UI)
│   │       ├── Web.csproj
│   │       ├── Program.cs
│   │       └── Pages/
│   │           ├── ViewImage.cshtml
│   │           ├── ViewImage.cshtml.cs
│   │           └── Shared/
│   │
│   └── Driven/                             # SAIDA (quem o Core chama)
│       ├── Postgres/                       # Adaptador de banco de dados
│       │   ├── Postgres.csproj             # EF Core + Npgsql
│       │   ├── DependencyInjection.cs
│       │   ├── Contexts/
│       │   │   ├── ApplicationDbContext.cs  # Implementa IApplicationDbContext
│       │   │   ├── UserContext.cs           # Implementa IUserContext
│       │   │   └── Helpers/
│       │   │       └── HttpContextClaimsHelper.cs
│       │   ├── Configurations/             # EF Core Fluent API
│       │   │   ├── EntityConfiguration.cs          # Base
│       │   │   ├── RemovableEntityConfiguration.cs # Base c/ soft delete
│       │   │   ├── Users/
│       │   │   │   ├── UserConfiguration.cs
│       │   │   │   └── Logins/
│       │   │   │       └── UserLoginConfiguration.cs
│       │   │   └── Images/
│       │   │       ├── ImageConfiguration.cs
│       │   │       ├── ImageLinkConfiguration.cs
│       │   │       └── ViewConfiguration.cs
│       │   ├── DomainEvents/
│       │   │   ├── EventsDispatcher.cs     # Implementa IEventsDispatcher
│       │   │   └── IEventsDispatcher.cs
│       │   ├── Options/
│       │   │   └── PostgresOptions.cs
│       │   └── Migrations/                 # EF Core Migrations
│       │
│       ├── Storage.MinIO/                  # Adaptador de object storage
│       │   ├── Storage.MinIO.csproj
│       │   ├── DependencyInjection.cs
│       │   ├── Configurations/
│       │   │   └── MinIOOptions.cs
│       │   └── Strategies/
│       │       └── MinIOImageLinkStrategy.cs  # Implementa IImageLinkStrategy
│       │
│       └── Messaging/
│           └── RabbitMQ/                   # Adaptador de mensageria
│               ├── RabbitMQ.csproj
│               ├── Configuration/
│               │   ├── QueueConfiguration.cs
│               │   └── RabbitMqOptions.cs
│               ├── Connection/
│               │   ├── IRabbitMqConnectionFactory.cs
│               │   └── RabbitMqConnectionFactory.cs
│               ├── Consumers/
│               │   └── ImageViewedEventConsumer.cs
│               ├── Publisher/
│               │   └── RabbitMqMessagePublisher.cs  # Implementa IMessagePublisher
│               ├── Serialization/
│               │   ├── IMessageSerializer.cs
│               │   └── JsonMessageSerializer.cs
│               ├── Setup/
│               │   ├── IQueueSetup.cs
│               │   └── QueueSetup.cs
│               └── Extensions/
│                   └── ServiceCollectionExtensions.cs
│
└── Infrastructure/                         # Composicao e servicos cross-cutting
    ├── Infrastructure.csproj
    ├── DependencyInjection.cs              # Orquestra TODOS os registros
    ├── Configurations/
    │   ├── AuthOptions.cs                  # JWT config
    │   └── EnvironmentOptions.cs
    ├── Services/
    │   ├── PasswordHasher.cs               # Implementa IPasswordHasher (BCrypt)
    │   └── TokenGenerator.cs               # Implementa ITokenGenerator (JWT)
    ├── Factories/
    │   ├── LoginStrategyFactory.cs         # Implementa ILoginStrategyFactory
    │   └── ImageLinkStrategyFactory.cs     # Implementa IImageLinkStrategyFactory
    ├── Managers/
    │   └── ApplicationLinkManager.cs       # Implementa IApplicationLinkManager
    ├── Strategies/Login/
    │   ├── LocalLoginStrategy.cs           # Implementa ILoginStrategy
    │   ├── GoogleLoginStrategy.cs
    │   ├── GitHubLoginStrategy.cs
    │   └── FacebookLoginStrategy.cs
    ├── Policies/
    │   ├── Policies.cs                     # Constantes de policies
    │   └── UserActive/
    │       ├── UserActiveRequirement.cs
    │       └── UserActiveHandler.cs
    └── Helpers/
        └── ClaimHelper.cs
```

---

## 3. Camada Domain (Core)

A camada mais interna do hexagono. Contem **apenas** logica de negocio pura. **Zero dependencias externas**.

### 3.1 Entity Base

Toda entidade herda de `Entity`, que fornece:

```csharp
public abstract class Entity
{
    private readonly List<IEvent> _events = [];

    // GUID v7 (time-sorted) gerado automaticamente
    public Guid Id { get; protected init; } = Guid.CreateVersion7();

    // Timestamp UTC automatico
    public DateTimeOffset CreatedAt { get; protected init; } = DateTimeOffset.UtcNow;

    // Domain Events
    public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();
    public void Raise(IEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}
```

### 3.2 RemovableEntity (Soft Delete)

Entidades que suportam exclusao logica herdam de `RemovableEntity`:

```csharp
public abstract class RemovableEntity : Entity
{
    public bool IsRemoved { get; private set; } = false;

    public Result Remove()
    {
        if (IsRemoved)
            return RemovableEntityErrors.AlreadyRemoved;

        IsRemoved = true;
        return Result.Ok();
    }

    public Result Restore()
    {
        if (!IsRemoved)
            return RemovableEntityErrors.NotRemoved;

        IsRemoved = false;
        return Result.Ok();
    }
}
```

### 3.3 Aggregate Root - Exemplo (User)

Aggregates sao a unidade de consistencia. Encapsulam regras de negocio e protegem invariantes:

```csharp
public sealed class User : RemovableEntity
{
    private readonly List<Image> _images = [];
    private readonly List<UserLogin> _logins = [];

    // Construtor privado para EF Core
    private User() { }

    // Propriedades com setters privados (encapsulamento)
    public string Name { get; private set; } = string.Empty;
    public string? PhotoUrl { get; private set; }
    public Email MailEmail { get; private set; } = null!;  // Value Object
    public UserStatus Status { get; private set; }
    public IReadOnlyList<Image> Images => _images.AsReadOnly();
    public IReadOnlyList<UserLogin> Logins => _logins.AsReadOnly();

    // Factory Method - unica forma de criar User
    public static Result<User> Create(
        string name, string? photoUrl, Email mainEmail,
        UserLoginProvider provider, string? providerUserId, string? password)
    {
        // Validacao de dominio
        if (string.IsNullOrWhiteSpace(name))
            return UserErrors.NameRequired;

        var loginResult = UserLogin.Create(provider, providerUserId, mainEmail, password);
        if (loginResult.IsFailure)
            return loginResult.Error;

        var user = new User(name, photoUrl, new Email(mainEmail.Address), UserStatus.Active);
        user._logins.Add(loginResult.Value);

        return user;  // implicit conversion para Result<User>
    }

    // Metodos de dominio retornam Result
    public Result Block()
    {
        if (Status is not UserStatus.Active)
            return UserErrors.NotActive;

        Status = UserStatus.Blocked;
        return Result.Ok();
    }

    public Result<Image> AddImage(string url, ImageLinkProvider provider)
    {
        if (Status is not UserStatus.Active)
            return UserErrors.NotActive;

        var imageResult = Image.Create(url, provider);
        if (imageResult.IsFailure)
            return imageResult.Error;

        _images.Add(imageResult.Value);
        return imageResult.Value;
    }
}
```

### 3.4 Value Objects

Objetos imutaveis que representam conceitos do dominio com validacao embutida:

```csharp
public sealed record Email
{
    public string Address { get; }

    // Construtor interno (usado pelo factory e EF Core)
    public Email(string address) => Address = address;

    // Factory com validacao
    public static Result<Email> Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return Error.Validation("Email.Empty", "Email is required");

        if (!Regex.IsMatch(address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Error.Validation("Email.Invalid", "Email is invalid");

        return new Email(address);
    }

    // Conversao implicita para string
    public static implicit operator string(Email email) => email.Address;
}
```

### 3.5 Domain Errors

Erros de dominio sao definidos como constantes estaticas por entidade:

```csharp
public static class UserErrors
{
    public static readonly Error NameRequired =
        Error.Validation("User.NameRequired", "Name is required");

    public static readonly Error NotActive =
        Error.Problem("User.NotActive", "User is not active");

    public static readonly Error EmailInUse =
        Error.Conflict("User.EmailInUse", "Email already in use");
}
```

### 3.6 Error Record

```csharp
public record Error(string Code, string Description, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    // Mapeamento automatico para HTTP status codes
    public int StatusCode => Type switch
    {
        ErrorType.Failure    => 500,
        ErrorType.Validation => 400,
        ErrorType.Problem    => 400,
        ErrorType.NotFound   => 404,
        ErrorType.Conflict   => 409,
        _ => 418
    };

    // Factories
    public static Error Failure(string code, string desc) => new(code, desc, ErrorType.Failure);
    public static Error NotFound(string code, string desc) => new(code, desc, ErrorType.NotFound);
    public static Error Problem(string code, string desc) => new(code, desc, ErrorType.Problem);
    public static Error Conflict(string code, string desc) => new(code, desc, ErrorType.Conflict);
}
```

### 3.7 Specification Pattern

Para queries reutilizaveis no dominio:

```csharp
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}
```

### 3.8 Organizacao por Feature (Domain)

```
Domain/
├── Users/              # Tudo relacionado ao aggregate User
│   ├── User.cs
│   ├── UserStatus.cs
│   ├── UserErrors.cs
│   ├── Logins/         # Sub-entidade dentro do aggregate
│   └── Specifications/ # Specs especificas do User
├── Images/             # Tudo relacionado ao aggregate Image
│   ├── Image.cs
│   ├── Links/
│   └── Views/
├── RefreshTokens/      # Entidade standalone
└── Shared/             # Building blocks reutilizaveis
    ├── Entities/
    ├── Results/
    ├── Events/
    ├── Specifications/
    └── ValueObjects/
```

---

## 4. Camada Application (Core)

Orquestra os casos de uso. Define os **Ports** (interfaces) que a infraestrutura deve implementar.

### 4.1 CQRS - Interfaces de Messaging

```csharp
// Markers para Commands
public interface ICommand;                    // Command sem retorno
public interface ICommand<TResponse>;         // Command com retorno

// Markers para Queries
public interface IQuery<TResponse>;           // Query sempre retorna algo

// Handlers
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command, CancellationToken ct);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken ct);
}

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken ct);
}
```

### 4.2 Anatomia de um Command (Vertical Slice)

Cada caso de uso fica em sua propria pasta com todos os arquivos necessarios:

```
Commands/Users/Create/
├── CreateUserCommand.cs      # DTO de entrada
├── CreateUserHandler.cs      # Logica do caso de uso
├── CreateUserResponse.cs     # DTO de saida
└── CreateUserValidator.cs    # FluentValidation
```

**Command (DTO de entrada):**
```csharp
public sealed record CreateUserCommand(
    string Name,
    string Email,
    UserLoginProvider Provider,
    string? ProviderUserId,
    string? Password) : ICommand<CreateUserResponse>;
```

**Response (DTO de saida):**
```csharp
public sealed record CreateUserResponse(Guid UserId, string Token, string RefreshToken);
```

**Handler (caso de uso):**
```csharp
internal sealed class CreateUserHandler(
    IApplicationDbContext context,     // Port: banco de dados
    ITokenGenerator tokenGenerator,   // Port: geracao de tokens
    IPasswordHasher passwordHasher)   // Port: hash de senhas
    : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> HandleAsync(
        CreateUserCommand command, CancellationToken cancellationToken)
    {
        // 1. Verificar regras de negocio
        var existsByEmail = await context.UserLogins
            .AnyAsync(x => x.Email.Address == command.Email, cancellationToken);
        if (existsByEmail)
            return UserErrors.EmailInUse;

        // 2. Criar value objects
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;

        // 3. Hash de senha (se local)
        var hashedPassword = command.Password is not null
            ? passwordHasher.Hash(command.Password)
            : null;

        // 4. Criar aggregate via factory method
        var userResult = User.Create(
            command.Name, string.Empty, emailResult.Value,
            command.Provider, command.ProviderUserId, hashedPassword);
        if (userResult.IsFailure)
            return userResult.Error;

        // 5. Persistir
        await context.Users.AddAsync(userResult.Value, cancellationToken);

        // 6. Gerar tokens
        var (authToken, refreshToken) = await tokenGenerator.SignInAsync(
            userResult.Value.Id, command.Email, userResult.Value.Status);

        // 7. Retornar resultado
        return new CreateUserResponse(userResult.Value.Id, authToken, refreshToken);
    }
}
```

**Validator (FluentValidation):**
```csharp
internal sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .When(x => x.Provider == UserLoginProvider.Local);
    }
}
```

### 4.3 Ports (Interfaces) - Application Layer

Os Ports sao as interfaces que definem o que o Core precisa do mundo externo:

```csharp
// Port: Acesso a dados (abstraindo o banco)
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserLogin> UserLogins { get; }
    DbSet<Image> Images { get; }
    DbSet<ImageLink> ImageLinks { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<View> Views { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
    Task PublishDomainEventsAsync(CancellationToken ct);
}

// Port: Usuario logado
public interface IUserContext
{
    Guid UserId { get; }
    UserStatus UserStatus { get; }
    string SessionType { get; }
    IHeaderDictionary Headers { get; }
    string? GetIpAddress();
}

// Port: Servicos
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}

public interface ITokenGenerator
{
    Task<(string Token, string RefreshToken)> SignInAsync(
        Guid userId, string email, UserStatus status);
}

// Port: Messaging
public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, CancellationToken ct);
}

// Port: Domain Events
public interface IEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IEvent> events, CancellationToken ct);
}

// Port: Strategies
public interface IImageLinkStrategy
{
    Task<(string Url, ImageLinkProvider Provider)> StoreAsync(byte[] image);
}

public interface ILoginStrategy
{
    Task<Result<User>> ExecuteAsync(LoginCommand command, CancellationToken ct);
}
```

---

## 5. Camada Infrastructure

Responsavel pela **composicao** de todo o sistema e implementacao de servicos cross-cutting.

### 5.1 DependencyInjection.cs (Composition Root)

Este arquivo orquestra o registro de TODAS as dependencias:

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddProjectDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration);       // Options pattern
        services.AddApplication();                 // Handlers + Validators + Decorators
        services.AddInfrastructure(configuration); // Strategies + Factories + Policies
        services.AddServices();                    // TokenGenerator, PasswordHasher
        services.AddPostgresAdapter(configuration);// EF Core + DbContext

        return services;
    }

    private static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStorageAdapters(configuration);  // MinIO
        services.AddLoginStrategies();               // Local, Google, GitHub, Facebook

        services.AddScoped<IImageLinkStrategyFactory, ImageLinkStrategyFactory>();
        services.AddScoped<ILoginStrategyFactory, LoginStrategyFactory>();
        services.AddPolicies();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IApplicationLinkManager, ApplicationLinkManager>();
        return services;
    }
}
```

### 5.2 Options Pattern

Toda configuracao externa usa o pattern `IOptions<T>` com validacao:

```csharp
// Definicao
public sealed class AuthOptions
{
    public const string SectionName = "Auth";
    public JwtOptions Jwt { get; init; } = null!;
}

// Registro (com validacao no startup)
services.AddOptions<AuthOptions>()
    .Bind(configuration.GetSection(AuthOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

---

## 6. Driving Adapters (Adaptadores de Entrada)

Sao os pontos de entrada da aplicacao. Recebem requisicoes externas e delegam para o Core.

### 6.1 API - Minimal APIs

Cada endpoint implementa a interface `IEndpoint`:

```csharp
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
```

**Exemplo de Endpoint:**

```csharp
internal sealed class Create : IEndpoint
{
    // Request DTO local ao endpoint
    private sealed record Request(
        string Name, string Email,
        UserLoginProvider Provider,
        string? ProviderUserId, string? Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (
                [FromBody] Request request,
                [FromServices] ICommandHandler<CreateUserCommand, CreateUserResponse> handler,
                CancellationToken cancellationToken) =>
            {
                // 1. Mapear Request para Command
                var command = new CreateUserCommand(
                    request.Name, request.Email,
                    request.Provider, request.ProviderUserId, request.Password);

                // 2. Executar via handler
                var result = await handler.HandleAsync(command, cancellationToken);

                // 3. Converter Result<T> para HTTP response
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
```

### 6.2 Auto-Discovery de Endpoints

Endpoints sao registrados automaticamente via reflection:

```csharp
// Registro no DI (Program.cs)
builder.Services.AddEndpoints(typeof(Program).Assembly);

// Mapeamento automatico
app.MapEndpoints();

// Implementacao do auto-discovery
public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
{
    var serviceDescriptors = assembly.DefinedTypes
        .Where(type => type is { IsAbstract: false, IsInterface: false }
                       && type.IsAssignableTo(typeof(IEndpoint)))
        .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
        .ToArray();

    services.TryAddEnumerable(serviceDescriptors);
    return services;
}

public static IApplicationBuilder MapEndpoints(this WebApplication app,
    RouteGroupBuilder? routeGroupBuilder = null)
{
    var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
    IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

    foreach (var endpoint in endpoints)
        endpoint.MapEndpoint(builder);

    return app;
}
```

### 6.3 Result Extensions (Match Pattern)

Converte `Result<T>` para `IResult` HTTP:

```csharp
public static TOut Match<TIn, TOut>(
    this Result<TIn> result,
    Func<TIn, TOut> onSuccess,
    Func<Result<TIn>, TOut> onFailure)
{
    return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
}

// Uso: result.Match(Results.Ok, CustomResults.Problem)
```

### 6.4 CustomResults (ProblemDetails)

Formata erros como RFC 7807 ProblemDetails:

```csharp
public static IResult Problem(Result result)
{
    return Results.Problem(
        title: GetTitle(result.Error),
        detail: GetDetail(result.Error),
        type: GetType(result.Error.Type),
        statusCode: result.Error.StatusCode,
        extensions: GetErrors(result));
}
```

### 6.5 Program.cs (Bootstrap)

```csharp
var builder = WebApplication.CreateBuilder(args);

// 1. Registrar endpoints (auto-discovery)
builder.Services.AddEndpoints(typeof(Program).Assembly);

// 2. Configurar JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* ... */ });

builder.Services.AddAuthorization();

// 3. Swagger
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

// 4. Todas as dependencias do projeto (composition root)
builder.Services.AddProjectDependencies(builder.Configuration);

// 5. CORS
builder.Services.AddCors(options => { /* ... */ });

var app = builder.Build();

// 6. Middleware pipeline
app.UseSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();  // Mapeia todos os IEndpoint
app.UseCors();

await app.RunAsync();
```

---

## 7. Driven Adapters (Adaptadores de Saida)

Implementam os Ports definidos no Core. O Core nao sabe qual tecnologia esta sendo usada.

### 7.1 Postgres Adapter (EF Core)

**ApplicationDbContext** implementa `IApplicationDbContext`:

```csharp
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserLogin> UserLogins => Set<UserLogin>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<ImageLink> ImageLinks => Set<ImageLink>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<View> Views => Set<View>();

    public override async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        // 1. Converter datas para UTC
        // 2. Proteger CreatedAt de modificacao
        // 3. Salvar
        var result = await base.SaveChangesAsync(ct);
        // 4. Publicar domain events
        await PublishDomainEventsAsync(ct);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configuracoes do assembly automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
```

**Entity Configuration Base:**

```csharp
// Configuracao base para todas as entidades
public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}

// Configuracao para entidades com soft delete
public abstract class RemovableEntityConfiguration<T>
    : EntityConfiguration<T> where T : RemovableEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.IsRemoved).IsRequired().HasDefaultValue(false);
        builder.HasQueryFilter(e => !e.IsRemoved);  // Global filter
    }
}
```

### 7.2 MinIO Adapter (Object Storage)

```csharp
public sealed class MinIOImageLinkStrategy : IImageLinkStrategy
{
    public async Task<(string Url, ImageLinkProvider Provider)> StoreAsync(byte[] image)
    {
        // 1. Criar bucket se nao existir
        // 2. Definir policy de acesso publico
        // 3. Gerar path organizado: images/{year}/{month}/{day}/{guid}.jpg
        // 4. Fazer upload
        // 5. Retornar URL publica
        return (publicUrl, ImageLinkProvider.MinIO);
    }
}
```

### 7.3 RabbitMQ Adapter (Mensageria)

```csharp
public sealed class RabbitMqMessagePublisher : IMessagePublisher
{
    public async Task PublishAsync<T>(T message, CancellationToken ct)
    {
        // 1. Obter canal AMQP
        // 2. Derivar routing key do tipo da mensagem (camelCase)
        // 3. Serializar com JSON
        // 4. Configurar propriedades (persistent, ContentType, Timestamp)
        // 5. Publicar na exchange
    }
}
```

### 7.4 Padrao de DependencyInjection por Adapter

Cada adapter tem seu proprio `DependencyInjection.cs`:

```csharp
// Postgres/DependencyInjection.cs
public static IServiceCollection AddPostgresAdapter(
    this IServiceCollection services, IConfiguration configuration)
{
    var options = configuration.GetSection(PostgresOptions.SectionName).Get<PostgresOptions>()!;

    services.AddDbContext<ApplicationDbContext>(opts =>
        opts.UseNpgsql(options.Connection));

    services.AddScoped<IApplicationDbContext>(sp =>
        sp.GetRequiredService<ApplicationDbContext>());

    services.AddScoped<IUserContext, UserContext>();
    services.AddScoped<IEventsDispatcher, EventsDispatcher>();

    return services;
}

// Storage.MinIO/DependencyInjection.cs
public static IServiceCollection AddMinIOAdapter(
    this IServiceCollection services, IConfiguration configuration)
{
    // Registrar MinIO client e strategy
}
```

---

## 8. Fluxo de Dependencias

### 8.1 Referencias entre Projetos (.csproj)

```
Domain.csproj           → Nenhuma referencia (independente)
Application.csproj      → Domain
Postgres.csproj         → Application, Domain
Storage.MinIO.csproj    → Application, Domain
RabbitMQ.csproj         → Application, Domain
Infrastructure.csproj   → Application, Domain, Postgres, Storage.MinIO
API.csproj              → Infrastructure (que traz tudo transitivamente)
Web.csproj              → Infrastructure
```

### 8.2 Diagrama de Dependencias

```
         ┌──────────────┐
         │   Domain     │  ← Nao depende de nada
         └──────┬───────┘
                │
         ┌──────▼───────┐
         │  Application │  ← Depende so do Domain
         └──────┬───────┘
                │
    ┌───────────┼───────────────────┐
    │           │                   │
┌───▼────┐ ┌───▼──────┐    ┌───────▼──────┐
│Postgres│ │Storage   │    │Infrastructure│
│        │ │MinIO     │    │              │
│        │ │          │    │(Composition  │
│        │ │RabbitMQ  │    │ Root)        │
└────────┘ └──────────┘    └──────┬───────┘
                                  │
                           ┌──────▼───────┐
                           │  API / Web   │  ← Ponto de entrada
                           └──────────────┘
```

### 8.3 Dependency Inversion na Pratica

```
ERRADO (dependencia direta):
  Handler → ApplicationDbContext (classe concreta do Postgres)

CORRETO (inversao de dependencia):
  Handler → IApplicationDbContext (interface no Application)
                  ↑
  ApplicationDbContext implementa IApplicationDbContext (no Postgres adapter)
```

---

## 9. Design Patterns Utilizados

| Pattern | Onde | Para que |
|---------|------|---------|
| **Hexagonal Architecture** | Estrutura geral | Isolar dominio de infraestrutura |
| **CQRS** | Commands/Queries | Separar leitura de escrita |
| **Result Pattern** | Domain/Application | Tratamento de erros sem exceptions |
| **Repository (implicito)** | DbContext DbSets | Acesso a dados via EF Core |
| **Specification** | Domain/Specifications | Queries reutilizaveis e compostas |
| **Strategy** | Login/ImageLink | Multiplas implementacoes intercambiaveis |
| **Factory** | LoginStrategyFactory | Criar strategy correta baseada no provider |
| **Decorator** | Behaviors | Cross-cutting concerns (validacao, transacao, log) |
| **Value Object** | Email, Phone | Validacao embutida, imutabilidade |
| **Domain Events** | Entity.Raise() | Comunicacao entre aggregates |
| **Aggregate Root** | User, Image | Unidade de consistencia transacional |
| **Vertical Slice** | Commands/Queries/Endpoints | Cada feature isolada em sua pasta |
| **Ports & Adapters** | Interfaces/Implementacoes | Inversao de dependencia |

---

## 10. CQRS - Commands e Queries

### Fluxo de um Command

```
HTTP Request
    ↓
Endpoint (Driving Adapter)
    ↓ cria Command
ICommandHandler<TCommand, TResponse>  ← Injetado via DI
    ↓ (na verdade e o decorator mais externo)
TransactionDecorator.HandleAsync()    ← Abre TransactionScope
    ↓
ValidationDecorator.HandleAsync()     ← Valida com FluentValidation
    ↓
LoggingDecorator.HandleAsync()        ← Loga execucao
    ↓
CreateUserHandler.HandleAsync()       ← Logica real do caso de uso
    ↓ retorna Result<T>
TransactionDecorator                  ← Commit se sucesso
    ↓
Endpoint                              ← Match(Ok, Problem)
    ↓
HTTP Response (200 ou ProblemDetails)
```

### Fluxo de uma Query

```
HTTP Request
    ↓
Endpoint (Driving Adapter)
    ↓ cria Query
IQueryHandler<TQuery, TResponse>      ← Injetado via DI
    ↓
GetUserByIdHandler.HandleAsync()      ← Logica de consulta
    ↓ retorna Result<T>
Endpoint                              ← Match(Ok, Problem)
    ↓
HTTP Response
```

**Nota:** Queries NAO passam pelos decorators (sem validacao, sem transacao).
Isso e intencional: queries sao operacoes de leitura simples.

---

## 11. Decorator Pattern (Cross-Cutting Concerns)

### 11.1 Ordem de Execucao dos Decorators

Registrados via Scrutor no `DependencyInjection.cs`:

```csharp
// A ordem de registro define a ordem de execucao (de fora para dentro)
services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionDecorator.CommandHandler<,>));
```

Resultado: `Transaction → Validation → Logging → Handler Real`

### 11.2 ValidationDecorator

```csharp
internal static class ValidationDecorator
{
    public sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(
            TCommand command, CancellationToken ct)
        {
            // 1. Validar todos os validators registrados
            var failures = await ValidateAsync(command, validators);

            // 2. Se passou, delegar para o proximo handler na cadeia
            if (failures.Length is 0)
                return await innerHandler.HandleAsync(command, ct);

            // 3. Se falhou, retornar erro de validacao
            return CreateValidationError(failures);
        }
    }
}
```

### 11.3 TransactionDecorator

```csharp
internal sealed class CommandHandler<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> innerHandler)
    : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    public async Task<Result<TResponse>> HandleAsync(
        TCommand command, CancellationToken ct)
    {
        // Abre escopo transacional
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var result = await innerHandler.HandleAsync(command, ct);

        // Commit apenas se sucesso OU se MustComplete
        if (result.IsSuccess || result.MustComplete)
            scope.Complete();

        return result;
    }
}
```

### 11.4 LoggingDecorator

```csharp
internal sealed class CommandHandler<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> innerHandler)
    : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    public async Task<Result<TResponse>> HandleAsync(
        TCommand command, CancellationToken ct)
    {
        var commandName = typeof(TCommand).Name;
        Console.WriteLine($"[INFO] Executing command: {commandName}");

        var result = await innerHandler.HandleAsync(command, ct);

        if (result.IsSuccess)
            Console.WriteLine($"[INFO] Command {commandName} executed successfully");
        else
            Console.WriteLine($"[ERROR] Command {commandName} failed: {result.Error.Code}");

        return result;
    }
}
```

---

## 12. Result Pattern (Railway-Oriented Programming)

### 12.1 Estrutura do Result

```csharp
// Result base (sem valor de retorno)
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public bool MustComplete { get; }  // Forca commit mesmo em caso de erro
    public int StatusCode => IsSuccess ? 200 : Error.StatusCode;

    public static Result Ok() => new(true, Error.None);

    // Conversao implicita: Error → Result (failure)
    public static implicit operator Result(Error error) => new(false, error);
}

// Result<T> (com valor de retorno)
public class Result<T> : Result
{
    public T Value { get; }  // So acessivel se IsSuccess

    // Conversoes implicitas
    public static implicit operator Result<T>(Error error) => Failure(error);
    public static implicit operator Result<T>(T value) => Ok(value);
}
```

### 12.2 Uso no Handler

```csharp
public async Task<Result<CreateUserResponse>> HandleAsync(...)
{
    // Retornar erro -> conversao implicita Error → Result<T>
    if (existsByEmail)
        return UserErrors.EmailInUse;  // implicit operator

    // Retornar sucesso -> conversao implicita T → Result<T>
    return new CreateUserResponse(userId, token, refreshToken);  // implicit operator
}
```

### 12.3 Uso no Endpoint

```csharp
var result = await handler.HandleAsync(command, ct);
return result.Match(
    onSuccess: Results.Ok,           // 200 + body
    onFailure: CustomResults.Problem // 4xx/5xx + ProblemDetails
);
```

---

## 13. Strategy e Factory Patterns

### 13.1 Login Strategy

```
ILoginStrategy (Port - Application)
    ├── LocalLoginStrategy    (Infrastructure) → email + password
    ├── GoogleLoginStrategy   (Infrastructure) → Google OAuth
    ├── GitHubLoginStrategy   (Infrastructure) → GitHub OAuth
    └── FacebookLoginStrategy (Infrastructure) → Facebook OAuth

ILoginStrategyFactory (Port - Application)
    └── LoginStrategyFactory  (Infrastructure) → Resolve pelo provider
```

### 13.2 Image Link Strategy

```
IImageLinkStrategy (Port - Application)
    └── MinIOImageLinkStrategy (Driven Adapter) → Armazena no MinIO

IImageLinkStrategyFactory (Port - Application)
    └── ImageLinkStrategyFactory (Infrastructure) → Resolve pelo provider
```

### 13.3 Factory Implementation

```csharp
public sealed class LoginStrategyFactory(IServiceProvider serviceProvider)
    : ILoginStrategyFactory
{
    public ILoginStrategy Create(UserLoginProvider provider) => provider switch
    {
        UserLoginProvider.Local    => serviceProvider.GetRequiredService<LocalLoginStrategy>(),
        UserLoginProvider.Google   => serviceProvider.GetRequiredService<GoogleLoginStrategy>(),
        UserLoginProvider.GitHub   => serviceProvider.GetRequiredService<GitHubLoginStrategy>(),
        UserLoginProvider.Facebook => serviceProvider.GetRequiredService<FacebookLoginStrategy>(),
        _ => throw new ArgumentOutOfRangeException(nameof(provider))
    };
}
```

---

## 14. Registro de Dependencias (DI)

### 14.1 Application Layer (Handlers + Validators)

```csharp
public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddHandlers();     // Auto-scan com Scrutor
    services.AddValidators();   // Auto-scan FluentValidation
    services.AddDecorators();   // Scrutor Decorate

    return services;
}

// Auto-scan de handlers
private static IServiceCollection AddHandlers(this IServiceCollection services)
{
    services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

    return services;
}

// Auto-scan de validators
private static IServiceCollection AddValidators(this IServiceCollection services)
{
    services.AddValidatorsFromAssembly(
        typeof(DependencyInjection).Assembly,
        includeInternalTypes: true);  // Permite validators internal
    return services;
}

// Decorators (Scrutor)
private static IServiceCollection AddDecorators(this IServiceCollection services)
{
    services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
    services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
    services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionDecorator.CommandHandler<,>));
    return services;
}
```

### 14.2 Pacotes NuGet Chave

| Pacote | Onde | Para que |
|--------|------|---------|
| `Scrutor` | Application | Auto-scan + Decorator pattern no DI |
| `FluentValidation` | Application | Validacao de Commands |
| `FluentValidation.DependencyInjectionExtensions` | Application | Auto-registro de validators |
| `Microsoft.EntityFrameworkCore` | Application (interface), Postgres (impl) | ORM |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | Postgres | Provider PostgreSQL |
| `Minio` | Storage.MinIO | Client S3-compatible |
| `RabbitMQ.Client` | RabbitMQ | Client AMQP |
| `BCrypt.Net-Next` | Infrastructure | Hash de senhas |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | API | Autenticacao JWT |
| `Swashbuckle.AspNetCore` | API | Swagger/OpenAPI |

---

## 15. Endpoints (Minimal APIs)

### 15.1 Padrao de Implementacao

Todo endpoint segue este template:

```csharp
internal sealed class NomeDoEndpoint : IEndpoint
{
    // Request DTO local (nao polui o namespace global)
    private sealed record Request(string Campo1, int Campo2);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("rota/do/endpoint", async (
                [FromBody] Request request,                          // Input
                [FromServices] ICommandHandler<XCommand, XResponse> handler, // Handler
                CancellationToken cancellationToken) =>
            {
                // 1. Mapear Request → Command
                var command = new XCommand(request.Campo1, request.Campo2);

                // 2. Executar
                var result = await handler.HandleAsync(command, cancellationToken);

                // 3. Converter para HTTP response
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.NomeDoGrupo)   // Agrupamento no Swagger
            .HasPermission(Policies.UserActive);  // Autorizacao (opcional)
    }
}
```

### 15.2 Endpoints Existentes

| Metodo | Rota | Handler | Descricao |
|--------|------|---------|-----------|
| POST | `/auth/login` | LoginHandler | Autenticar usuario |
| POST | `/auth/refresh-token` | RefreshTokenHandler | Renovar JWT |
| POST | `/users` | CreateUserHandler | Criar usuario |
| GET | `/users/{id}` | GetUserByIdHandler | Buscar usuario por ID |
| POST | `/images` | CreateImageHandler | Upload de imagem |

---

## 16. Configuracoes e Docker

### 16.1 appsettings.json

```json
{
  "Auth": {
    "Jwt": {
      "Secret": "sua-chave-secreta-hs256-minimo-32-chars",
      "Issuer": "NomeDoProjeto",
      "Audience": "NomeDoProjeto",
      "DurationInMinutes": 60
    }
  },
  "Postgres": {
    "Connection": "Host=localhost;Database=nomedb;Username=user;Password=pass"
  },
  "MinIO": {
    "Endpoint": "localhost:9000",
    "AccessKey": "minioadmin",
    "SecretKey": "minioadmin",
    "UseSSL": false,
    "BucketName": "images",
    "Region": "us-east-1"
  },
  "Environment": {
    "Name": "Development"
  }
}
```

### 16.2 docker-compose.yml

```yaml
services:
  postgres:
    image: postgres:17-alpine
    container_name: projeto-postgres
    environment:
      POSTGRES_DB: nomedb
      POSTGRES_USER: user
      POSTGRES_PASSWORD: password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U user -d nomedb"]

  minio:
    image: minio/minio:latest
    container_name: projeto-minio
    environment:
      MINIO_ROOT_USER: minioadmin
      MINIO_ROOT_PASSWORD: minioadmin
    ports:
      - "9000:9000"    # API
      - "9001:9001"    # Console
    volumes:
      - minio_data:/data
    command: server /data --console-address ":9001"

volumes:
  postgres_data:
  minio_data:

networks:
  default:
    driver: bridge
```

---

## 17. Guia Passo a Passo para Migrar outro Projeto

### Passo 1: Criar a Estrutura de Pastas

```bash
mkdir -p src/Core/Domain/Shared/{Entities,Results,Events,Specifications,ValueObjects,Errors}
mkdir -p src/Core/Application/{Commands,Queries,Services,Strategies}
mkdir -p src/Core/Application/Shared/{Behaviors,Contexts,Events,Extensions,Managers,Messaging,Ports}
mkdir -p src/Adapters/Driving/API/{Endpoints,Extensions,Infra}
mkdir -p src/Adapters/Driven/Postgres/{Configurations,Contexts,DomainEvents,Migrations,Options}
mkdir -p src/Infrastructure/{Configurations,Services,Factories,Strategies,Policies}
```

### Passo 2: Criar os Projetos (.csproj)

```bash
# Core
dotnet new classlib -n Domain -o src/Core/Domain --framework net10.0
dotnet new classlib -n Application -o src/Core/Application --framework net10.0

# Adapters
dotnet new webapi -n API -o src/Adapters/Driving/API --framework net10.0
dotnet new classlib -n Postgres -o src/Adapters/Driven/Postgres --framework net10.0

# Infrastructure
dotnet new classlib -n Infrastructure -o src/Infrastructure --framework net10.0
```

### Passo 3: Configurar Referencias entre Projetos

```xml
<!-- Application.csproj -->
<ProjectReference Include="..\..\Domain\Domain.csproj" />

<!-- Postgres.csproj -->
<ProjectReference Include="..\..\Core\Application\Application.csproj" />

<!-- Infrastructure.csproj -->
<ProjectReference Include="..\Core\Application\Application.csproj" />
<ProjectReference Include="..\Adapters\Driven\Postgres\Postgres.csproj" />

<!-- API.csproj -->
<ProjectReference Include="..\..\Infrastructure\Infrastructure.csproj" />
```

### Passo 4: Copiar Building Blocks (Domain/Shared)

Copie estes arquivos como estao - sao reutilizaveis entre projetos:

1. `Entity.cs` - Classe base com Id (Guid v7) e CreatedAt
2. `RemovableEntity.cs` - Soft delete
3. `Result.cs` - Result pattern
4. `Error.cs` - Error record
5. `ErrorType.cs` - Enum de tipos de erro
6. `ValidationError.cs` - Erro de validacao
7. `IEvent.cs` / `IEventHandler.cs` - Domain events
8. `ISpecification.cs` - Specification pattern

### Passo 5: Copiar Interfaces de Messaging (Application/Shared/Messaging)

1. `ICommand.cs`
2. `ICommandHandler.cs`
3. `IQuery.cs`
4. `IQueryHandler.cs`

### Passo 6: Copiar Decorators (Application/Shared/Behaviors)

1. `ValidationDecorator.cs`
2. `TransactionDecorator.cs`
3. `LoggingDecorator.cs`

### Passo 7: Definir Ports (Interfaces)

Adapte para seu dominio:

1. `IApplicationDbContext.cs` - Com os DbSets do seu dominio
2. `IUserContext.cs` - Se tiver autenticacao

### Passo 8: Criar Entidades do Dominio

Para cada aggregate, crie na pasta correspondente:
- Entidade principal (Aggregate Root)
- Sub-entidades
- Value Objects
- Enums
- Errors (constantes estaticas)

### Passo 9: Criar Commands e Queries

Para cada caso de uso, crie a pasta com:
- `XCommand.cs` (record com dados de entrada, implementa `ICommand<XResponse>`)
- `XHandler.cs` (classe que implementa `ICommandHandler<XCommand, XResponse>`)
- `XResponse.cs` (record com dados de saida)
- `XValidator.cs` (FluentValidation, opcional)

### Passo 10: Implementar Adapters

- **Postgres:** DbContext, Configurations, Migrations
- **Outros:** Conforme necessario (MinIO, RabbitMQ, etc.)

### Passo 11: Configurar DI

1. `Application/DependencyInjection.cs` - Handlers + Validators + Decorators
2. `Postgres/DependencyInjection.cs` - DbContext + Contexts
3. `Infrastructure/DependencyInjection.cs` - Orquestra tudo
4. `API/Program.cs` - Chama `AddProjectDependencies()`

### Passo 12: Criar Endpoints

1. Copiar `IEndpoint.cs`, `Tags.cs`
2. Copiar `EndpointExtensions.cs`, `ResultExtensions.cs`, `CustomResults.cs`
3. Criar endpoints seguindo o template da secao 15.1

### Passo 13: Instalar Pacotes NuGet

```bash
# Application
dotnet add Application package Scrutor
dotnet add Application package FluentValidation
dotnet add Application package FluentValidation.DependencyInjectionExtensions
dotnet add Application package Microsoft.EntityFrameworkCore  # para DbSet<T>

# Postgres
dotnet add Postgres package Microsoft.EntityFrameworkCore
dotnet add Postgres package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add Postgres package Microsoft.AspNetCore.Http.Abstractions

# Infrastructure
dotnet add Infrastructure package BCrypt.Net-Next

# API
dotnet add API package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add API package Swashbuckle.AspNetCore
```

---

## Checklist Final de Migracao

- [ ] Estrutura de pastas criada
- [ ] Projetos .csproj criados com target framework correto
- [ ] Referencias entre projetos configuradas
- [ ] Building blocks copiados (Entity, Result, Error, etc.)
- [ ] Interfaces CQRS copiadas (ICommand, IQuery, IHandler)
- [ ] Decorators copiados (Validation, Transaction, Logging)
- [ ] Ports definidos (IApplicationDbContext, IUserContext, etc.)
- [ ] Entidades do dominio criadas com factory methods
- [ ] Value Objects com validacao
- [ ] Errors definidos como constantes
- [ ] Commands e Queries com handlers e validators
- [ ] DbContext implementando IApplicationDbContext
- [ ] Entity Configurations (Fluent API)
- [ ] Endpoints implementando IEndpoint
- [ ] Auto-discovery de endpoints configurado
- [ ] CustomResults e ResultExtensions copiados
- [ ] DI configurado em cada camada
- [ ] Infrastructure orquestrando tudo
- [ ] Program.cs com middleware pipeline
- [ ] appsettings.json configurado
- [ ] docker-compose.yml configurado
- [ ] Pacotes NuGet instalados
- [ ] `dotnet build` passando sem erros

---

## Tecnologias e Versoes

| Tecnologia | Versao | Uso |
|-----------|--------|-----|
| .NET | 10.0 | Runtime |
| C# | 13 | Linguagem |
| EF Core | 10.0 (RC2) | ORM |
| PostgreSQL | 17 | Banco de dados |
| MinIO | Latest | Object storage |
| RabbitMQ | Latest | Mensageria |
| FluentValidation | 12.x | Validacao |
| Scrutor | Latest | DI scanning + Decorators |
| BCrypt.Net | Latest | Hash de senhas |
| JWT Bearer | Latest | Autenticacao |
| Swashbuckle | Latest | Swagger |
