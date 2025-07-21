using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioConviteErrors
{
    public static readonly Error TokenInvalido =
        new("UsuarioConvite.TokenInvalido", ErrorType.Validation, "Token inválido.");

    public static readonly Error StatusInvalidoParaAceitar =
        new("UsuarioConvite.StatusInvalidoParaAceitar", ErrorType.Validation, "Apenas convites com a situação pendente podem ser aceitos.");
    
    public static readonly Error StatusInvalidoParaRecusar =
        new("UsuarioConvite.StatusInvalidoParaRecusar", ErrorType.Validation, "Apenas convites com a situação pendente podem ser recusados.");

    public static readonly Error StatusInvalidoParaExpirar =
        new("UsuarioConvite.StatusInvalidoParaExpirar", ErrorType.Validation, "Apenas convites com a situação pendente podem ser expirados.");
    
    public static readonly Error DataExpiracaoNaoAtingida =
        new("UsuarioConvite.DataExpiracaoNaoAtingida", ErrorType.Validation, "A data de expiração do convite não foi atingida, não é possível expirá-lo.");
    
    public static readonly Error StatusInvalidoParaCancelar =
        new("UsuarioConvite.StatusInvalidoParaCancelar", ErrorType.Validation, "Apenas convites com a situação pendente podem ser cancelados.");
    
    public static readonly Error MotivoObrigatorio =
        new("UsuarioConvite.MotivoObrigatorio", ErrorType.Validation, "O motivo do cancelamento do convite é obrigatório.");
    
    public static readonly Error ConviteNaoEncontrado =
        new("UsuarioConvite.ConviteNaoEncontrado", ErrorType.NotFound, "Convite não encontrado.");
    
    public static readonly Error ConvitePendente =
        new("UsuarioConvite.ConvitePendente", ErrorType.Validation, "Já existe um convite pendente.");
    
    public static readonly Error StatusInvalidoParaPendenciar =
        new("UsuarioConvite.StatusInvalidoParaPendenciar", ErrorType.Validation, "Apenas convites com a situação enviado podem ser pendenciados.");
}