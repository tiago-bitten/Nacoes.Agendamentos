using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;
public sealed class Atividade : EntityId<Atividade>
{
    #region Constructors
    private Atividade() { }

    internal Atividade(string nome, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome));
        }

        Nome = nome;
        Descricao = descricao;
    }
    #endregion

    public string Nome { get; private set; } = null!;
    public string? Descricao { get; private set; }
}
