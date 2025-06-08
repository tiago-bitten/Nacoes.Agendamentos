using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;

public static class VoluntarioMapper
{
    public static Voluntario GetEntidade(this AdicionarVoluntarioCommand command)
        => new(command.Nome,
               string.IsNullOrEmpty(command.Email) ? null : new Email(command.Email),
               command.Celular == null ? null : new Celular(command.Celular.Ddd, command.Celular.Numero),
               string.IsNullOrEmpty(command.Cpf) ? null : new CPF(command.Cpf),
               command.DataNascimento == null ? null : new DataNascimento(command.DataNascimento.Value));
}
