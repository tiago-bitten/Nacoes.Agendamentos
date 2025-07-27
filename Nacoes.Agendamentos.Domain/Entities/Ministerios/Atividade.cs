using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;
public sealed class Atividade : EntityId
{
    private Atividade() { }

    private Atividade(string nome, string? descricao = null)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public string Nome { get; private set; } = null!;
    public string? Descricao { get; private set; }

    internal static Result<Atividade> Criar(string nome, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return AtividadeErrors.NomeObrigatorio;
        }
        
        var atividade = new Atividade(nome, descricao);
        return Result<Atividade>.Success(atividade);
    }
}
