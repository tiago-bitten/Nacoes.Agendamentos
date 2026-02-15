using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Ministerios;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AddMinistryHandler(
    INacoesDbContext context)
    : ICommandHandler<AddMinistryCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddMinistryCommand command,
        CancellationToken ct)
    {
        var ministryResult = Ministry.Create(
            command.Name,
            command.Description,
            new Color(command.Color.Value, command.Color.Type));

        if (ministryResult.IsFailure)
        {
            return ministryResult.Error;
        }

        var ministry = ministryResult.Value;

        await context.Ministries.AddAsync(ministry, ct);
        await context.SaveChangesAsync(ct);

        return ministry.Id;
    }
}
