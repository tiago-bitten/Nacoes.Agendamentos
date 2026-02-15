using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UserAddedDomainEvent(Guid UserId) : IDomainEvent;
