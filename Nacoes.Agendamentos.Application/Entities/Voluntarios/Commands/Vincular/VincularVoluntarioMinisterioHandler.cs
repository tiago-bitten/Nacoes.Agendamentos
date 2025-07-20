using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;
public sealed class VincularVoluntarioMinisterioHandler(IUnitOfWork uow,
                                                        IValidator<VincularVoluntarioMinisterioCommand> commandValidator,
                                                        IVoluntarioRepository voluntarioRepository,
                                                        IMinisterioRepository ministerioRepository)
    : ICommandHandler<VincularVoluntarioMinisterioCommand>
{
    public async Task<Result> Handle(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        var commandResult = await commandValidator.CheckAsync(command, cancellation);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }

        var voluntario = await voluntarioRepository.GetByIdAsync(command.VoluntarioId, includes: "Ministerios");
        if (voluntario is null)
        {
            return VoluntarioErrors.NaoEncontrado;
        }

        var ministerioId = await ministerioRepository.GetByIdToProject(command.MinisterioId)
                                                     .Select(x => x.Id)
                                                     .FirstOrDefaultAsync(cancellation);
        if (ministerioId is null)
        {
            return MinisterioErrors.NaoEncontrado;
        }
        
        await uow.BeginAsync();
        var result = voluntario.VincularMinisterio(ministerioId);
        
        if (result.IsFailure)
        {
            return result.Error;
        }
        await uow.CommitAsync(cancellation);
      
        return Result.Success();
    }
}
