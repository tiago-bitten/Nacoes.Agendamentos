using Domain.Shared.Events;

namespace Domain.Ministerios.DomainEvents;

public sealed record MinisterioAdicionadoDomainEvent(Guid MinisterioId) : IDomainEvent;
