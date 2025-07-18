using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public static class VoluntarioErrors
{
    public static readonly Error NaoEncontrado = 
        new("Voluntario.NaoEncontrado", "Voluntario não encontrado.");
}