using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VoluntarioAdicionadoDomainEvent(Guid VoluntarioId) : IDomainEvent;
