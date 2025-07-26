using Nacoes.Agendamentos.Domain.Abstracts;
using UsuarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.Usuario>;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

public sealed record UsuarioAdicionadoDomainEvent(UsuarioId UsuarioId) : IDomainEvent;
