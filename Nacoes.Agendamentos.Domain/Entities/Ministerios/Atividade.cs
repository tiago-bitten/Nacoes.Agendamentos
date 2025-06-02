using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;
public sealed class Atividade : EntityId<Atividade>
{
    #region Constructors
    internal Atividade() { }

    internal Atividade(string nome, string? descricao = default)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome));
        }

        Nome = nome;
        Descricao = descricao;
    }
    #endregion

    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
}
