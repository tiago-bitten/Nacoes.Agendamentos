using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class VoluntarioAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<VoluntarioAdicionadoDomainEvent>
{
    public Task Handle(VoluntarioAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.VoluntarioId, acao: "Voluntário adicionado.", EHistoricoTipoAcao.Criar);
    }
}