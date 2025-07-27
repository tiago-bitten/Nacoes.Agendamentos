using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas.DomainEvents;

public sealed record AgendamentoAdicionadoDomainEvent(Guid AgendamentoId) : IDomainEvent;