using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UsuarioAdicionadoDomainEvent(Guid UsuarioId) : IDomainEvent;
