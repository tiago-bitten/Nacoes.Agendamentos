using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.AdicionarAgendamento;

internal sealed record AdicionarAgendamentoCommand : ICommand<Guid>
{
    public Guid AgendaId { get; init; }
    public Guid VoluntarioMinisterioId { get; init; }
    public Guid AtividadeId { get; init; }
}
