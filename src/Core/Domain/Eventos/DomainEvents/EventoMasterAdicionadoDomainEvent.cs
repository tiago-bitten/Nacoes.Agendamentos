using Domain.Shared.Events;

namespace Domain.Eventos.DomainEvents;

public sealed record EventoMasterAdicionadoDomainEvent(Guid EventoId) : IDomainEvent;
