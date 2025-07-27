using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

internal sealed class UsuarioConviteRecusadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<UsuarioConviteRecusadoDomainEvent>
{
    public Task Handle(UsuarioConviteRecusadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.UsuarioConviteId, acao: "Convite recusado.");
    }
}