using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Voluntarios.DomainEvents;
using Domain.Voluntarios.Errors;
using Domain.Voluntarios.Specs;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class UnlinkVolunteerMinistryHandler(
    INacoesDbContext context)
    : ICommandHandler<UnlinkVolunteerMinistryCommand>
{
    public async Task<Result> HandleAsync(
        UnlinkVolunteerMinistryCommand command,
        CancellationToken ct)
    {
        var volunteerMinistry = await context.Volunteers
            .ApplySpec(new VolunteerByMinistryLinkSpec(command.VolunteerMinistryId))
            .Select(x => new
            {
                Volunteer = x,
                Ministry = x.Ministries
                    .Select(y => new
                    {
                        Id = y.MinistryId,
                        y.Ministry.Name
                    }).Single()
            }).FirstOrDefaultAsync(ct);

        if (volunteerMinistry is null)
        {
            return VolunteerErrors.NotFound;
        }

        var volunteer = volunteerMinistry.Volunteer;
        var ministry = volunteerMinistry.Ministry;

        var unlinkResult = volunteer.UnlinkMinistry(ministry.Id);
        if (unlinkResult.IsFailure)
        {
            return unlinkResult.Error;
        }

        volunteer.Raise(new VolunteerMinistryUnlinkedDomainEvent(volunteer.Id, ministry.Name));
        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
