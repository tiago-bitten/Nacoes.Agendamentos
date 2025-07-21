using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public static class MinisterioErrors
{
    public static readonly Error NaoEncontrado = 
        new("Ministerio.NaoEncontrado", ErrorType.NotFound, "Ministerio não encontrado.");
}