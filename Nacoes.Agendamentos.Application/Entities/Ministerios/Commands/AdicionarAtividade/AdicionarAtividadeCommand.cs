using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed record AdicionarAtividadeCommand : ICommand<Id<Atividade>>
{
    public required string Nome { get; init; }
    public string? Descricao { get; init; }
    
    public Guid MinisterioId { get; init; }
}
