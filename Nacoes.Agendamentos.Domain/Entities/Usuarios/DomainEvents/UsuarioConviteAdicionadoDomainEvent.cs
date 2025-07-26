using Nacoes.Agendamentos.Domain.Abstracts;
using UsuarioConviteId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.UsuarioConvite>;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

public sealed record UsuarioConviteAdicionadoDomainEvent(UsuarioConviteId UsuarioConviteId, string LinkConvite) : IDomainEvent;