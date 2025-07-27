using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

public sealed record UsuarioConviteAceitoDomainEvent(Guid UsuarioConviteId) : IDomainEvent;