using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioConviteErrors
{
    public static readonly Error TokenInvalido =
        new("UsuarioConvite.TokenInvalido", "Token inválido.");

    public static readonly Error StatusInvalidoParaAceitar =
        new("UsuarioConvite.StatusInvalidoParaAceitar", "Apenas convites com a situação pendente podem ser aceitos.");
    
    public static readonly Error StatusInvalidoParaRecusar =
        new("UsuarioConvite.StatusInvalidoParaRecusar", "Apenas convites com a situação pendente podem ser recusados.");

    public static readonly Error StatusInvalidoParaExpirar =
        new("UsuarioConvite.StatusInvalidoParaExpirar", "Apenas convites com a situação pendente podem ser expirados.");
    
    public static readonly Error DataExpiracaoNaoAtingida =
        new("UsuarioConvite.DataExpiracaoNaoAtingida", "A data de expiração do convite não foi atingida, não é possível expirá-lo.");
    
    public static readonly Error StatusInvalidoParaCancelar =
        new("UsuarioConvite.StatusInvalidoParaCancelar", "Apenas convites com a situação pendente podem ser cancelados.");
    
    public static readonly Error MotivoObrigatorio =
        new("UsuarioConvite.MotivoObrigatorio", "O motivo do cancelamento do convite é obrigatório.");
    
    public static readonly Error ConviteNaoEncontrado =
        new("UsuarioConvite.ConviteNaoEncontrado", "Convite não encontrado.");
    
    public static readonly Error ConvitePendente =
        new("UsuarioConvite.ConvitePendente", "Já existe um convite pendente.");
    
    public static readonly Error StatusInvalidoParaPendenciar =
        new("UsuarioConvite.StatusInvalidoParaPendenciar", "Apenas convites com a situação enviado podem ser pendenciados.");
}