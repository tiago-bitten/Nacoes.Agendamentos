using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgendamento;

internal sealed class AdicionarAgendamentoHandler(IUnitOfWork uow,
                                                  IAgendaRepository agendaRepository,
                                                  IVoluntarioMinisterioRepository voluntarioMinisterioRepository,
                                                  IAtividadeRepository atividadeRepository)
    : ICommandHandler<AdicionarAgendamentoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AdicionarAgendamentoCommand command, CancellationToken cancellation = default)
    {
        // agendamento.Raise(new AgendamentoAdicionadoDomainEvent(agendamento.Id));
        throw new NotImplementedException();
    }
}