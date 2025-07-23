using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

public static class VoluntarioMinisterioErrors
{
    public static readonly Error VoluntarioJaEstaAtivo = 
        Error.Problem("VoluntarioMinisterio.VoluntarioJaEstaAtivo", "Voluntario já possui vinculo com esse ministério.");
    
    public static readonly Error VoluntarioJaEstaSuspenso = 
        Error.Problem("VoluntarioMinisterio.VoluntarioJaEstaSuspenso", "Voluntario já possui vinculo com esse ministério.");
}