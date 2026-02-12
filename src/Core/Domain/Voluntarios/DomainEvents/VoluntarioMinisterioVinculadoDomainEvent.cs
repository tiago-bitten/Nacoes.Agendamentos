using Domain.Shared.Events;

namespace Domain.Voluntarios.DomainEvents;

public sealed record VoluntarioMinisterioVinculadoDomainEvent(Guid VoluntarioId, string NomeMinisterio) : IDomainEvent;
