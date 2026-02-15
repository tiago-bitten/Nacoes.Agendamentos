using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Entities.Voluntarios.Mappings;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Voluntarios.Specs;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class AddVolunteerHandler(
    INacoesDbContext context)
    : ICommandHandler<AddVolunteerCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddVolunteerCommand command,
        CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(command.Email))
        {
            var volunteerWithEmailExists = await context.Volunteers
                .ApplySpec(new VolunteerWithEmailAddressSpec(command.Email))
                .AnyAsync(ct);
            if (volunteerWithEmailExists)
            {
            }
        }

        var volunteerResult = command.ToDomain();
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error;
        }

        var volunteer = volunteerResult.Value;
        await context.Volunteers.AddAsync(volunteer, ct);
        await context.SaveChangesAsync(ct);

        return volunteer.Id;
    }
}
