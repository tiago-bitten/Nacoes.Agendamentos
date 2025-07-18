using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public static class VoluntarioMinisterioErrors
{
    public static readonly Error VoluntarioJaEstaAtivo = 
        new("VoluntarioMinisterio.VoluntarioJaEstaAtivo", "Voluntario já possui vinculo com esse ministério.");
    
    public static readonly Error VoluntarioJaEstaSuspenso = 
        new("VoluntarioMinisterio.VoluntarioJaEstaSuspenso", "Voluntario já possui vinculo com esse ministério.");
}