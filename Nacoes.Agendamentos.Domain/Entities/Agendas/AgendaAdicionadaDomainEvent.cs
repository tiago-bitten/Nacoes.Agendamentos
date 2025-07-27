using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed record AgendaAdicionadaDomainEvent(Guid AgendaId) : IDomainEvent;

