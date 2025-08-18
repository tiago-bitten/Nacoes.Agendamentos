using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;

internal sealed class VincularVoluntarioMinisterioHandler(
    INacoesDbContext context, 
    IVoluntarioRepository voluntarioRepository, 
    IMinisterioRepository ministerioRepository)
    : ICommandHandler<VincularVoluntarioMinisterioCommand>
{
    public async Task<Result> Handle(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        var voluntario = await voluntarioRepository.GetByIdAsync(command.VoluntarioId, includes: "Ministerios");
        if (voluntario is null)
        {
            return VoluntarioErrors.NaoEncontrado;
        }

        var ministerio = await ministerioRepository.GetByIdToProject(command.MinisterioId)
                                                    .Select(x => new
                                                    {
                                                        x.Id,
                                                        x.Nome
                                                    })
                                                    .FirstOrDefaultAsync(cancellation);
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
