using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public sealed class Ministerio : BaseEntity<Ministerio>, IAggregateRoot
{
    #region Ctor
    internal Ministerio() { }

    public Ministerio(string nome, string descricao, Cor cor)
    {
        Nome = nome;
        Descricao = descricao;
        Cor = cor;
    }
    #endregion

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Cor Cor { get; private set; }
}
