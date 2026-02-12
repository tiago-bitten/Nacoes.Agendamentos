using Domain.Shared.Events;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Voluntarios.DomainEvents;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class VoluntarioAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<VoluntarioAdicionadoDomainEvent>
{
    public Task Handle(VoluntarioAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: "Voluntário adicionado.");
    }
}
