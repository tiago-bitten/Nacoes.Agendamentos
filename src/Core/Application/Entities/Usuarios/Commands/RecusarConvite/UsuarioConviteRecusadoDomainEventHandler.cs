using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class UserInvitationDeclinedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<UserInvitationDeclinedDomainEvent>
{
    public Task Handle(UserInvitationDeclinedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.UserInvitationId, action: "Invitation declined.");
    }
}
