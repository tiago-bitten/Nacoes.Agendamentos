using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

public static class VoluntarioMinisterioErrors
{
    public static readonly Error VoluntarioJaEstaVinculado = 
        Error.Problem("VoluntarioMinisterio.VoluntarioJaEstaVinculado", "Voluntario já possui vinculo com esse ministério.");
    
    public static readonly Error VoluntarioJaEstaDesvinculado = 
        Error.Problem("VoluntarioMinisterio.VoluntarioJaEstaDesvinculado", "Voluntario já está desvinculado com esse ministério.");
    
    public static readonly Error VoluntarioJaEstaSuspenso = 
        Error.Problem("VoluntarioMinisterio.VoluntarioJaEstaSuspenso", "Voluntario já possui vinculo com esse ministério.");
    
    public static readonly Error VoluntarioNaoEstaVinculadoAoMinisterio = 
        Error.Problem("VoluntarioMinisterio.VoluntarioNaoEstaVinculadoAoMinisterio", "Voluntario não possui vinculo com esse ministério.");
}