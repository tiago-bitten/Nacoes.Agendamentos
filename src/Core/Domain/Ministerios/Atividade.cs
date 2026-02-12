using Domain.Shared.Entities;
using Domain.Shared.Results;

namespace Domain.Ministerios;

public sealed class Atividade : RemovableEntity
{
    private Atividade() { }

    private Atividade(string nome, string? descricao = null)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public string Nome { get; private set; } = string.Empty;
    public string? Descricao { get; private set; }
    public Guid MinisterioId { get; private set; }

    public Ministerio Ministerio { get; private set; } = null!;

    internal static Result<Atividade> Criar(string nome, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return AtividadeErrors.NomeObrigatorio;
        }

        return new Atividade(nome, descricao);
    }
}
