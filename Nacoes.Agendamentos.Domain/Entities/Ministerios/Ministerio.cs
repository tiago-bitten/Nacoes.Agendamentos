using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public sealed class Ministerio : EntityId<Ministerio>, IAggregateRoot
{
    #region Construtores
    internal Ministerio() { }

    public Ministerio(string nome, string descricao, Cor? cor = default)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome), "O nome do ministerio é obrigatorio.");
        }

        Nome = nome;
        Descricao = descricao;
        Cor = cor ?? Cor.Default;
    }
    #endregion

    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public Cor Cor { get; private set; }

    private IList<Atividade> _atividades = [];
    public IReadOnlyCollection<Atividade> Atividades => _atividades.AsReadOnly();

    public void AtualizarNome(string nome) => Nome = nome;
    
    public void AtualizarDescricao(string descricao) => Descricao = descricao;
    
    public void AtualizarCor(Cor cor) => Cor = cor;

    public void AdicionarAtividade(string nome, string? descricao)
    {
        _atividades.Add(new Atividade(nome, descricao));
    }
}
