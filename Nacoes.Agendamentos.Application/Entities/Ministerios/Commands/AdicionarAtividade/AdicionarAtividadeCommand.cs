namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed record AdicionarAtividadeCommand
{
    public required string Nome { get; init; }
    public string? Descricao { get; init; }
}
