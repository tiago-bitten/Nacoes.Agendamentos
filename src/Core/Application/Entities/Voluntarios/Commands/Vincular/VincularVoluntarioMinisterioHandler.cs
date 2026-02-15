using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Ministerios;
using Domain.Voluntarios.DomainEvents;
using Domain.Voluntarios.Errors;

namespace Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class LinkVolunteerMinistryHandler(
    INacoesDbContext context)
    : ICommandHandler<LinkVolunteerMinistryCommand>
{
    public async Task<Result> HandleAsync(
        LinkVolunteerMinistryCommand command,
        CancellationToken ct)
    {
        var volunteerMinistryToLink = await context.Volunteers
            .Include(v => v.Ministries)
            .Where(v => v.Id == command.VolunteerId)
            .Select(v => new
            {
                Volunteer = v,
                Ministry = context.Ministries
                    .SingleOrDefault(m => m.Id == command.MinistryId)
            })
            .SingleOrDefaultAsync(ct);

        if (volunteerMinistryToLink is null)
        {
            return VolunteerErrors.NotFound;
        }

        var volunteer = volunteerMinistryToLink.Volunteer;
        var ministry = volunteerMinistryToLink.Ministry;

        if (ministry is null)
        {
            return MinistryErrors.NotFound;
        }

        var linkResult = volunteer.LinkMinistry(ministry.Id);
        if (linkResult.IsFailure)
        {
            return linkResult.Error;
        }

        volunteer.Raise(new VolunteerMinistryLinkedDomainEvent(volunteer.Id, ministry.Name));
        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
