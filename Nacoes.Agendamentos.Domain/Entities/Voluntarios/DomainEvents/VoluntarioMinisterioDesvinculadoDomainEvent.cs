using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;

public sealed record VoluntarioMinisterioDesvinculadoDomainEvent(Guid VoluntarioId, string NomeMinisterio) : IDomainEvent;
