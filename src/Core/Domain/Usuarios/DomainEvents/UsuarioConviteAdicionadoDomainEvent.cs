using Domain.Shared.Events;

namespace Domain.Usuarios.DomainEvents;

public sealed record UserInvitationAddedDomainEvent(Guid UserInvitationId, string InvitationLink) : IDomainEvent;
