using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record EventoAdicionadoDomainEvent(Guid EventoId) : IDomainEvent;
