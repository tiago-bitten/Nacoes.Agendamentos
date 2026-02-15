using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Ministerios;
using Domain.Ministerios.DomainEvents;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AddActivityHandler(
    INacoesDbContext context)
    : ICommandHandler<AddActivityCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddActivityCommand command,
        CancellationToken ct)
    {
        var ministry = await context.Ministries.SingleOrDefaultAsync(x => x.Id == command.MinistryId, ct);
        if (ministry is null)
        {
            return MinistryErrors.NotFound;
        }

        var activityWithNameExists = await context.Activities
            .AnyAsync(x => x.MinistryId == command.MinistryId && x.Name == command.Name, ct);
        if (activityWithNameExists)
        {
            return ActivityErrors.NameInUse;
        }

        var activityResult = ministry.AddActivity(command.Name, command.Description);
        if (activityResult.IsFailure)
        {
            return activityResult.Error;
        }

        var activity = activityResult.Value;

        ministry.Raise(new ActivityAddedDomainEvent(activity.Id));
        await context.SaveChangesAsync(ct);

        return Result<Guid>.Success(activity.Id);
    }
}
