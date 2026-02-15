using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UserInvitationDeclinedDomainEvent(Guid UserInvitationId) : IDomainEvent;
