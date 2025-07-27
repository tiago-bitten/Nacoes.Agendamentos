using UsuarioConviteId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.UsuarioConvite>;

using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

public sealed record UsuarioConviteRecusadoDomainEvent(UsuarioConviteId UsuarioConviteId) : IDomainEvent;
