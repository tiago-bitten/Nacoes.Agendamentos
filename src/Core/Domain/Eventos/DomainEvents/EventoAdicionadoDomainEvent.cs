using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record EventAddedDomainEvent(Guid EventId) : IDomainEvent;
