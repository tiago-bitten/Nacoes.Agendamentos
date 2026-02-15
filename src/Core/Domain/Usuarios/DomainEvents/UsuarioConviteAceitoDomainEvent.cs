using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UserInvitationAcceptedDomainEvent(Guid UserInvitationId) : IDomainEvent;
