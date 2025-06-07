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

    public Usuario(string nome, Email email, EAuthType authType, Celular? celular = default, string? senha = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome), "O nome do usuário é obrigatório.");
        }

        if (authType == EAuthType.Local && string.IsNullOrEmpty(senha))
        {
            throw ExceptionFactory.UsuarioSenhaObrigatoriaParaAuthLocal();
        }

        if (!string.IsNullOrEmpty(senha))
        {
            AtualizarSenha(senha);
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
    public Celular? Celular { get; private set; }
    public EAuthType AuthType { get; private set; }

    private readonly List<UsuarioAprovacao> _solicitacoes = [];
    public IReadOnlyCollection<UsuarioAprovacao> Solicitacoes => _solicitacoes.AsReadOnly();

    #region SolicitarAprovacao
    public void SolicitarAprovacao(IList<Id<Ministerio>> ministerios)
    {
        var ultima = _solicitacoes.LastOrDefault();

        if (ultima != null && !ultima.PodeSolicitar)
        {
            throw ExceptionFactory.UsuarioNaoPodeSolicitarPoisUltimaSolicitacaoNaoFoiReprovada();
        }

        var novaSolicitacao = new UsuarioAprovacao();
        novaSolicitacao.AdicionarMinisterios(ministerios);

        _solicitacoes.Add(novaSolicitacao);
    }
    #endregion

    #region AprovarUsuario
    public void AprovarUsuario(Usuario usuarioSolicitante, UsuarioAprovacao solicitacao, IList<Id<Ministerio>> ministerios)
    {
        solicitacao!.Aprovar(this, ministerios);
    }
    #endregion

    #region ReprovarUsuario
    public void ReprovarUsuario(Usuario usuarioSolicitante, UsuarioAprovacao solicitacao)
    {
        solicitacao!.Reprovar(this);
    }
    #endregion

    #region AtualizarSenha
    public void AtualizarSenha(string senha)
    {
        if (senha.Length < 6)
        {
            throw ExceptionFactory.UsuarioSenhaInferiorSeisDigitos();
        }

        Senha = senha;
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