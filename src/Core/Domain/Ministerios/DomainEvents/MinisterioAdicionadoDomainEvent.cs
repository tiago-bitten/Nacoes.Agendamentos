using Domain.Shared.Events;

namespace Domain.Ministerios.DomainEvents;

public sealed record MinistryAddedDomainEvent(Guid MinistryId) : IDomainEvent;
