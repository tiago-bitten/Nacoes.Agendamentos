using Domain.Shared.Events;

namespace Domain.Ministerios.DomainEvents;

public sealed record ActivityAddedDomainEvent(Guid ActivityId) : IDomainEvent;
