using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UsuarioConviteAdicionadoDomainEvent(Guid UsuarioConviteId, string LinkConvite) : IDomainEvent;
