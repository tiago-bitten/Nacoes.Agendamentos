using Application.Entities.Voluntarios.Commands.Adicionar;
using Domain.Shared.Results;
using Domain.Voluntarios;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Voluntarios.Mappings;

public static class VolunteerMapper
{
    public static Result<Volunteer> ToDomain(this AddVolunteerCommand command)
        => Volunteer.Create(
            command.Name,
            string.IsNullOrWhiteSpace(command.Email) ? null : new Email(command.Email),
            command.PhoneNumber is null ? null : new PhoneNumber(command.PhoneNumber.AreaCode, command.PhoneNumber.Number),
            string.IsNullOrWhiteSpace(command.Cpf) ? null : new CPF(command.Cpf),
            !command.BirthDate.HasValue ? null : new BirthDate(command.BirthDate.Value),
            command.RegistrationOrigin);
}
