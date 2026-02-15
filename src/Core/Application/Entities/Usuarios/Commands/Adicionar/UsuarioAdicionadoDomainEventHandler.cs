using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.Adicionar;

internal sealed class UserAddedDomainEventHandler(IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<UserAddedDomainEvent>
{
    public Task Handle(UserAddedDomainEvent domainEvent, CancellationToken ct)
    {
        return auditLogRegister.AuditAsync(domainEvent.UserId, action: "User added.");
    }
}
