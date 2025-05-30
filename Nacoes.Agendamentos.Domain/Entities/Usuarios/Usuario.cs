using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class Usuario : EntityId<Usuario>, IAggregateRoot
{
    #region Construtor
    internal Usuario() { }

    public Usuario(string nome, Email email, Celular celular, EAuthType authType, string? senha = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome), "O nome do usuário é obrigatório.");
        }

        Nome = nome;
        Email = email;
        Celular = celular;
        AuthType = authType;
        Senha = senha;
    }
    #endregion

    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string? Senha { get; private set; }
    public Celular Celular { get; private set; }
    public EAuthType AuthType { get; private set; }

    private readonly List<UsuarioAprovacao> _solicitacoes = [];
    public IReadOnlyCollection<UsuarioAprovacao> Solicitacoes => _solicitacoes.AsReadOnly();

    public bool EstaAprovado => _solicitacoes.Any(s => s.Status == EStatusAprovacao.Aprovado);


    #region SolicitarAprovacao
    public void SolicitarAprovacao(IList<Id<Ministerio>> ministerios)
    {
        var ultima = _solicitacoes.LastOrDefault();

        if (ultima != null && !ultima.PodeSolicitar)
        {
            Throw.UsuarioNaoPodeSolicitarPoisUltimaSolicitacaoNaoFoiReprovada();
        }

        var novaSolicitacao = new UsuarioAprovacao();
        novaSolicitacao.AdicionarMinisterios(ministerios);

        _solicitacoes.Add(novaSolicitacao);
    }
    #endregion

    #region AprovarUsuario
    public void AprovarUsuario(Usuario usuarioSolicitante, IList<Id<Ministerio>> ministerios)
    {
        if (!EstaAprovado)
        {
            Throw.UsuarioNaoPodeAprovarPoisSuaContaNaoFoiAprovada();
        }

        var solicitacao = usuarioSolicitante.Solicitacoes.LastOrDefault(s => s.Status == EStatusAprovacao.Aguardando);

        if (solicitacao == null)
        {
            Throw.UsuarioNenhumaSolicitacaoPendenteEncontrada();
        }

        solicitacao!.Aprovar(this, ministerios);
    }
    #endregion

    #region ReprovarUsuario
    public void ReprovarUsuario(Usuario usuarioSolicitante)
    {
        if (!EstaAprovado)
        {
            Throw.UsuarioNaoPodeReprovarPoisSuaContaNaoFoiAprovada();
        }

        var solicitacao = usuarioSolicitante.Solicitacoes.LastOrDefault(s => s.Status == EStatusAprovacao.Aguardando);

        if (solicitacao == null)
        {
            Throw.UsuarioNenhumaSolicitacaoPendenteEncontrada();
        }

        solicitacao!.Reprovar(this);
    }
    #endregion
}


public enum EAuthType
{
    Local = 1,
    Google = 2,
    Facebook = 3,
    Apple = 4
}