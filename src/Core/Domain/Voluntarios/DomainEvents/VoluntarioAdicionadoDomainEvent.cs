using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VolunteerAddedDomainEvent(Guid VolunteerId) : IDomainEvent;
