using Domain.Shared.Events;

namespace Domain.Ministerios.DomainEvents;

public sealed record AtividadeAdicionadaDomainEvent(Guid AtividadeId) : IDomainEvent;
