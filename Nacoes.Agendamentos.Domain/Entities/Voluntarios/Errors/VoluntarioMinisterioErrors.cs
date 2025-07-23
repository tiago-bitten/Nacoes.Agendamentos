using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

public static class VoluntarioMinisterioErrors
{
    public static readonly Error VoluntarioJaEstaAtivo = 
        new("VoluntarioMinisterio.VoluntarioJaEstaAtivo", ErrorType.Validation, "Voluntario já possui vinculo com esse ministério.");
    
    public static readonly Error VoluntarioJaEstaSuspenso = 
        new("VoluntarioMinisterio.VoluntarioJaEstaSuspenso", ErrorType.Validation, "Voluntario já possui vinculo com esse ministério.");
}