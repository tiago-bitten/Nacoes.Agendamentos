using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

internal sealed class UsuarioAdicionadoDomainEventHandler(IHistoricoRegister historicoRegister)
    : IDomainEventHandler<UsuarioAdicionadoDomainEvent>
{
    public Task Handle(UsuarioAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return historicoRegister.AuditAsync(domainEvent.UsuarioId, acao: "Usuário adicionado.", EHistoricoTipoAcao.Criar);
    }
}