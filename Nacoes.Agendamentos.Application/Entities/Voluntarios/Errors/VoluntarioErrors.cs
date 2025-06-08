using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Errors;

public static class VoluntarioErrors
{
    public readonly static Error VoluntarioComCpfExistente
        = new("Voluntario.Adicionar.CpfExistente", "Voluntário já cadastrado com o CPF informado.");

    public readonly static Error VoluntarioComEmailExistente
        = new("Voluntario.Adicionar.EmailExistente", "Voluntário já cadastrado com o email informado.");

    public readonly static Error VoluntarioJaPossuiMinisterioAtivo
        = new("Voluntario.VincularMinisterio.MinisterioJaAtivo", "Voluntário já está vinculado a esse ministério.");
}
