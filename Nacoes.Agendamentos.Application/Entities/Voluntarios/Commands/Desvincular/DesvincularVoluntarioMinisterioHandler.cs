using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

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
