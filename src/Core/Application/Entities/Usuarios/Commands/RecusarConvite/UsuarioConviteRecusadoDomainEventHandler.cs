using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class UsuarioConviteRecusadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<UsuarioConviteRecusadoDomainEvent>
{
    public Task Handle(UsuarioConviteRecusadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.UsuarioConviteId, acao: "Convite recusado.");
    }
}
