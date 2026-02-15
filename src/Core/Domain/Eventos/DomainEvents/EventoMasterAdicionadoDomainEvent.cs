using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record MasterEventAddedDomainEvent(Guid EventId) : IDomainEvent;
