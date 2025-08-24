using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public static class UsuarioMinisterioErrors
{
    public static readonly Error UsuarioObrigatorio 
        = Error.Problem("UsuarioMinisterio.UsuarioObrigatorio", "O usuário é obrigatório.");
    
    public static readonly Error MinisterioObrigatorio 
        = Error.Problem("UsuarioMinisterio.MinisterioObrigatorio", "O ministerio é obrigatório.");
    
    public static readonly Error UsuarioJaDesvinculadoDoMinisterio
        = Error.Problem("UsuarioMinisterio.UsuarioJaDesvinculadoDoMinisterio", "O usuário já foi desvinculado do ministerio.");
    
    public static readonly Error UsuarioJaVinculadoAoMinisterio
        = Error.Problem("UsuarioMinisterio.UsuarioJaVinculadoAoMinisterio", "O usuário já foi vinculado ao ministerio.");
}