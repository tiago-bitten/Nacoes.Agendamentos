using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VolunteerMinistryLinkedDomainEvent(Guid VolunteerId, string MinistryName) : IDomainEvent;
