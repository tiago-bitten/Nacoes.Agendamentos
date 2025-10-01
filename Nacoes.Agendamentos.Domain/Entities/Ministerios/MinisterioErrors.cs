using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public static class MinisterioErrors
{
    public static readonly Error NaoEncontrado = 
        Error.NotFound("Ministerio.NaoEncontrado", "Ministerio não encontrado.");
    
    public static readonly Error NomeObrigatorio = 
        Error.Problem("Ministerio.NomeObrigatorio", "O nome do ministerio é obrigatorio.");
    
    public static readonly Error CorObrigatoria = 
        Error.Problem("Ministerio.CorObrigatoria", "A cor do ministerio é obrigatoria.");
}