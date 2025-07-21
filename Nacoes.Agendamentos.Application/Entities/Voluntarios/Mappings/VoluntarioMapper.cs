using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;

public static class VoluntarioMapper
{
    public static Result<Voluntario> ToDomain(this AdicionarVoluntarioCommand command)
        => Voluntario.Criar(
            command.Nome,
            string.IsNullOrWhiteSpace(command.Email) ? null : new Email(command.Email),
            command.Celular is null ? null : new Celular(command.Celular.Ddd, command.Celular.Numero),
            string.IsNullOrWhiteSpace(command.Cpf) ? null : new CPF(command.Cpf),
            !command.DataNascimento.HasValue ? null : new DataNascimento(command.DataNascimento.Value),
            command.OrigemCadastro);
}
