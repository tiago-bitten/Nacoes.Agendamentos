using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record ReservationAddedDomainEvent(Guid ReservationId) : IDomainEvent;
