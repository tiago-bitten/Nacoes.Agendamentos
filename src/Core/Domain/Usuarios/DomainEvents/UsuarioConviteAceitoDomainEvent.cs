using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UsuarioConviteAceitoDomainEvent(Guid UsuarioConviteId) : IDomainEvent;
