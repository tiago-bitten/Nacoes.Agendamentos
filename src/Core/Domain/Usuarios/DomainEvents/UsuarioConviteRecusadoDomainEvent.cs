using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UsuarioConviteRecusadoDomainEvent(Guid UsuarioConviteId) : IDomainEvent;
