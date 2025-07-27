using Nacoes.Agendamentos.Domain.Abstracts;
using AgendamentoId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agendamento>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas.DomainEvents;

public sealed record AgendamentoAdicionadoDomainEvent(AgendamentoId AgendamentoId) : IDomainEvent;