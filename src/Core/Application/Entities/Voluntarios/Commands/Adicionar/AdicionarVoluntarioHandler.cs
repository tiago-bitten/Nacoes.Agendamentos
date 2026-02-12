using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Entities.Voluntarios.Mappings;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Voluntarios.Specs;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class AdicionarVoluntarioHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarVoluntarioCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        if (!string.IsNullOrEmpty(command.Email))
        {
            var existeVountarioComEmail = await context.Voluntarios
                .ApplySpec(new VoluntarioComEmailAddressSpec(command.Email))
                .AnyAsync(cancellation);
            if (existeVountarioComEmail)
            {
                // TODO: Add WarningContext, voluntario pode ter email duplicado, pois pode ser dependente
            }
        }

        var voluntarioResult = command.ToDomain();
        if (voluntarioResult.IsFailure)
        {
            return voluntarioResult.Error;
        }

        var voluntario = voluntarioResult.Value;
        await context.Voluntarios.AddAsync(voluntario, cancellation);
        await context.SaveChangesAsync(cancellation);

        return voluntario.Id;
    }
}
