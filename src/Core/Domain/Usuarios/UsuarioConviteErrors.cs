using Domain.Shared.Results;

namespace Domain.Usuarios;

public static class UsuarioConviteErrors
{
    public static readonly Error TokenInvalido =
        Error.Problem("UsuarioConvite.TokenInvalido", "Token inválido.");

    public static readonly Error StatusInvalidoParaAceitar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaAceitar", "Apenas convites com a situação pendente podem ser aceitos.");

    public static readonly Error StatusInvalidoParaRecusar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaRecusar", "Apenas convites com a situação pendente podem ser recusados.");

    public static readonly Error StatusInvalidoParaExpirar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaExpirar", "Apenas convites com a situação pendente podem ser expirados.");

    public static readonly Error DataExpiracaoNaoAtingida =
        Error.Problem("UsuarioConvite.DataExpiracaoNaoAtingida", "A data de expiração do convite não foi atingida, não é possível expirá-lo.");

    public static readonly Error StatusInvalidoParaCancelar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaCancelar", "Apenas convites com a situação pendente podem ser cancelados.");

    public static readonly Error MotivoObrigatorio =
        Error.Problem("UsuarioConvite.MotivoObrigatorio", "O motivo do cancelamento do convite é obrigatório.");

    public static readonly Error ConviteNaoEncontrado =
        Error.NotFound("UsuarioConvite.ConviteNaoEncontrado", "Convite não encontrado.");

    public static readonly Error ConvitePendente =
        Error.Problem("UsuarioConvite.ConvitePendente", "Já existe um convite pendente.");

    public static readonly Error StatusInvalidoParaPendenciar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaPendenciar", "Apenas convites com a situação enviado podem ser pendenciados.");

    public static readonly Error StatusInvalidoParaEnviar =
        Error.Problem("UsuarioConvite.StatusInvalidoParaEnviar", "Apenas convites com a situação gerado podem ser enviados.");

    public static readonly Error ConviteEnviado =
        Error.Problem("UsuarioConvite.ConviteEnviado", "Já existe um convite sendo enviado. Por favor, aguarde.");
}
