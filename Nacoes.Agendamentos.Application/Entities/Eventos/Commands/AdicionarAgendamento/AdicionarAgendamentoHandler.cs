using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.AdicionarAgendamento;

internal sealed class AdicionarAgendamentoHandler(
    INacoesDbContext context, 
    IVoluntarioMinisterioRepository voluntarioMinisterioRepository, 
    IAtividadeRepository atividadeRepository)
    : ICommandHandler<AdicionarAgendamentoCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarAgendamentoCommand command, CancellationToken cancellation = default)
    {
        // agendamento.Raise(new AgendamentoAdicionadoDomainEvent(agendamento.Id));
        throw new NotImplementedException();
    }
}