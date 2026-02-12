using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;
using Domain.Ministerios;
using Domain.Voluntarios.DomainEvents;
using Domain.Voluntarios.Errors;

namespace Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class VincularVoluntarioMinisterioHandler(
    INacoesDbContext context)
    : ICommandHandler<VincularVoluntarioMinisterioCommand>
{
    public async Task<Result> HandleAsync(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        var voluntarioMinisterioParaVincular = await context.Voluntarios
            .Include(v => v.Ministerios)
            .Where(v => v.Id == command.VoluntarioId)
            .Select(v => new
            {
                Voluntario = v,
                Ministerio = context.Ministerios
                    .SingleOrDefault(m => m.Id == command.MinisterioId)
            })
            .SingleOrDefaultAsync(cancellation);

        if (voluntarioMinisterioParaVincular is null)
        {
            return VoluntarioErrors.NaoEncontrado;
        }

        var voluntario = voluntarioMinisterioParaVincular.Voluntario;
        var ministerio = voluntarioMinisterioParaVincular.Ministerio;

        if (ministerio is null)
        {
            return MinisterioErrors.NaoEncontrado;
        }

        var vinculoResult = voluntario.VincularMinisterio(ministerio.Id);
        if (vinculoResult.IsFailure)
        {
            return vinculoResult.Error;
        }

        voluntario.Raise(new VoluntarioMinisterioVinculadoDomainEvent(voluntario.Id, ministerio.Nome));
        await context.SaveChangesAsync(cancellation);

        return Result.Success();
    }
}
