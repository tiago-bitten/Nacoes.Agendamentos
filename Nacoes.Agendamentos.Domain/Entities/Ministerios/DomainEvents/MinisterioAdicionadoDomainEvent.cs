using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

public sealed record MinisterioAdicionadoDomainEvent(Guid MinisterioId) : IDomainEvent;
