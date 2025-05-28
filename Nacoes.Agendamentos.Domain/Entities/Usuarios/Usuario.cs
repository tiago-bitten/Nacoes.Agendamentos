using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class Usuario : EntityId<Usuario>, IAggregateRoot
{
    #region Ctor
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
    public void SolicitarAprovacao()
    {
        var ultima = _solicitacoes.LastOrDefault();

        if (ultima != null && !ultima.PodeSolicitar)
        {
            throw new Exception("Não é possível solicitar aprovação neste momento.");
        }

        _solicitacoes.Add(new UsuarioAprovacao(this));
    }
    #endregion

    #region AprovarUsuario
    public void AprovarUsuario(Usuario usuarioSolicitante)
    {
        if (!EstaAprovado)
        {
            throw new Exception("Usuário não pode aprovar pois ainda não foi aprovado.");
        }

        var solicitacao = usuarioSolicitante.Solicitacoes.LastOrDefault(s => s.Status == EStatusAprovacao.Aguardando);

        if (solicitacao == null)
        {
            throw new Exception("Nenhuma solicitação pendente encontrada.");
        }

        solicitacao.Aprovar(this);
    }
    #endregion

    #region ReprovarUsuario
    public void ReprovarUsuario(Usuario usuarioSolicitante)
    {
        if (!EstaAprovado)
        {
            throw new Exception("Usuário não pode reprovar pois ainda não foi aprovado.");
        }

        var solicitacao = usuarioSolicitante.Solicitacoes.LastOrDefault(s => s.Status == EStatusAprovacao.Aguardando);

        if (solicitacao == null)
        {
            throw new Exception("Nenhuma solicitação pendente encontrada.");
        }

        solicitacao.Reprovar(this);
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