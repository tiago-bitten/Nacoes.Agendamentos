using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Voluntarios.DomainEvents;
using Domain.Voluntarios.Errors;
using Domain.Voluntarios.Specs;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class DesvincularVoluntarioMinisterioHandler(
    INacoesDbContext context)
    : ICommandHandler<DesvincularVoluntarioMinisterioCommand>
{
    public async Task<Result> HandleAsync(
        DesvincularVoluntarioMinisterioCommand command,
        CancellationToken cancellation = default)
    {
        var voluntarioMinisterio = await context.Voluntarios
            .ApplySpec(new VoluntarioPorVinculoMinisterioSpec(command.VoluntarioMinisterioId))
            .Select(x => new
            {
                Voluntario = x,
                Ministerio = x.Ministerios
                    .Select(y => new
                    {
                        Id = y.MinisterioId,
                        y.Ministerio.Nome
                    }).Single()
            }).FirstOrDefaultAsync(cancellation);

        if (voluntarioMinisterio is null)
        {
            return VoluntarioErrors.NaoEncontrado;
        }

        var voluntario = voluntarioMinisterio.Voluntario;
        var ministerio = voluntarioMinisterio.Ministerio;

        var vinculoResult = voluntario.DesvincularMinisterio(ministerio.Id);
        if (vinculoResult.IsFailure)
        {
            return vinculoResult.Error;
        }

        voluntario.Raise(new VoluntarioMinisterioDesvinculadoDomainEvent(voluntario.Id, ministerio.Nome));
        await context.SaveChangesAsync(cancellation);

        return Result.Success();
    }
}
