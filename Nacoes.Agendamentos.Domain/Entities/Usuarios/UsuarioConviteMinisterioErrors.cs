using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioConviteMinisterioErrors
{
    public static readonly Error ConviteObrigatorio = 
        Error.Problem("UsuarioConviteMinisterio.ConviteObrigatorio", "O convite é obrigatório.");
    
    public static readonly Error MinisterioObrigatorio = 
        Error.Problem("UsuarioConviteMinisterio.MinisterioObrigatorio", "O ministerio é obrigatório.");
}