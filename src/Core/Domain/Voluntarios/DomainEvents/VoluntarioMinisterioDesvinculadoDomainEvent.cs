using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VolunteerMinistryUnlinkedDomainEvent(Guid VolunteerId, string MinistryName) : IDomainEvent;
