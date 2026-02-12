using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VoluntarioMinisterioDesvinculadoDomainEvent(Guid VoluntarioId, string NomeMinisterio) : IDomainEvent;
