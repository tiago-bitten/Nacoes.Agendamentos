using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

public record AgendarCommand : ICommand<Id<Agendamento>>
{
    public Guid AgendaId { get; init; }
    public Guid VoluntarioMinisterioId { get; init; }
    public Guid AtividadeId { get; init; }
}
