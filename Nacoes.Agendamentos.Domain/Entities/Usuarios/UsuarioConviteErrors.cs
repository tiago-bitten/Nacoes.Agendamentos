using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioConviteErrors
{
    public static readonly Error TokenInvalido =
        new("UsuarioConvite.TokenInvalido", "Token inválido.");

    public static readonly Error StatusInvalidoParaAceitar =
        new("UsuarioConvite.StatusInvalidoParaAceitar", "Apenas convites com a situação enviado podem ser aceitos.");
    
    public static readonly Error StatusInvalidoParaRecusar =
        new("UsuarioConvite.StatusInvalidoParaRecusar", "Apenas convites com a situação enviado podem ser recusados.");

    public static readonly Error StatusInvalidoParaExpirar =
        new("UsuarioConvite.StatusInvalidoParaExpirar", "Apenas convites com a situação enviado podem ser expirados.");
    
    public static readonly Error DataExpiracaoNaoAtingida =
        new("UsuarioConvite.DataExpiracaoNaoAtingida", "A data de expiração do convite não foi atingida, não é possível expirá-lo.");
}