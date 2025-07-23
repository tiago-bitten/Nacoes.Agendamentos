using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios;

public static class MinisterioErrors
{
    public static readonly Error NaoEncontrado = 
        Error.NotFound("Ministerio.NaoEncontrado", "Ministerio não encontrado.");
}