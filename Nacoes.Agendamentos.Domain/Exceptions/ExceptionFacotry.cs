namespace Nacoes.Agendamentos.Domain.Exceptions;

public static class Throw
{
    #region UsuarioJaFoiAvaliado
    public static DomainException UsuarioJaFoiAvaliado()
        => new("Usuario.Avaliar", "O usuário já foi avaliado.");
    #endregion

    #region UsuarioNenhumaSolicitacaoEncontrada
    public static DomainException UsuarioNenhumaSolicitacaoPendenteEncontrada()
        => new("Usuario.Solicitar", "Nenhuma solicitação pendente encontrada.");
    #endregion

    #region UsuarioNaoPodeSolicitarPoisUltimaSolicitacaoNaoFoiReprovada
    public static DomainException UsuarioNaoPodeSolicitarPoisUltimaSolicitacaoNaoFoiReprovada()
        => new("Usuario.Solicitar", "Apenas é possível solicitar uma nova aprovação caso a anterior seja reprovada.");
    #endregion

    #region UsuarioNaoPodeAprovarPoisSuaContaNaoFoiAprovada
    public static DomainException UsuarioNaoPodeAprovarPoisSuaContaNaoFoiAprovada()
        => new("Usuario.Aprovar", "O usuário não pode aprovar pois sua conta não foi aprovada.");
    #endregion

    #region UsuarioNaoPodeReprovarPoisSuaContaNaoFoiAprovada
    public static DomainException UsuarioNaoPodeReprovarPoisSuaContaNaoFoiAprovada()
        => new("Usuario.Aprovar", "O usuário não pode reprovar pois sua conta não foi aprovada.");
    #endregion

    #region MinisterioSolicitanteJaFoiAprovado
    public static DomainException MinisterioSolicitanteJaFoiAprovado()
        => new("Usuario.UsuarioAprovacao.UsuarioMinisterioAprovacao.Aprovar", "O ministerio solicitante já foi aprovado.");
    #endregion

    #region VoluntarioJaEstaAtivo
    public static DomainException VoluntarioJaEstaAtivo()
        => new("Voluntario.Ativar", "O voluntario já está ativo.");
    #endregion

    #region VoluntarioJaEstaSuspenso
    public static DomainException VoluntarioJaEstaSuspenso()
        => new("Voluntario.Suspender", "O voluntario já está suspenso.");
    #endregion

    #region UsuarioNaoEncontrado
    public static DomainException UsuarioNaoEncontrado()
        => new("Login.Local", "E-mail inválido.");
    #endregion

    #region AutenticacaoInvalida
    public static DomainException AutenticacaTipoInvalido(string contaExterna)
        => new("Login.Local", $"Você precisa entrar com sua conta {contaExterna}.");
    #endregion

    #region SenhaInvalida
    public static DomainException SenhaInvalida()
        => new("Login.Local", "Senha inválida.");
    #endregion
}
