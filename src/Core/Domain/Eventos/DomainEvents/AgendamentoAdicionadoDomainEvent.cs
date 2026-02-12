using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record AgendamentoAdicionadoDomainEvent(Guid AgendamentoId) : IDomainEvent;
