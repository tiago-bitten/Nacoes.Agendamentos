using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

internal sealed class DesvincularVoluntarioMinisterioHandler(INacoesDbContext context,
                                                             IVoluntarioRepository voluntarioRepository)
    : ICommandHandler<DesvincularVoluntarioMinisterioCommand>
{
    public async Task<Result> Handle(DesvincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        var voluntario = await voluntarioRepository.RecuperarPorVoluntarioMinisterio(command.VoluntarioMinisterioId)
                                                   .SingleOrDefaultAsync(cancellation);
        if (voluntario is null)
        {
            return VoluntarioErrors.NaoEncontrado;
        }

        var ministerio = voluntario.Ministerios.Select(x => x.Ministerio).SingleOrDefault();
        if (ministerio is null)
        {
            return MinisterioErrors.NaoEncontrado;
        }
        
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
