using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;
public sealed class VincularVoluntarioMinisterioHandler(IUnitOfWork uow,
                                                        IValidator<VincularVoluntarioMinisterioCommand> vincularVoluntarioMinisterioValidator,
                                                        IVoluntarioRepository voluntarioRepository,
                                                        IMinisterioRepository ministerioRepository)
    : BaseHandler(uow), IVincularVoluntarioMinisterioHandler
{
    public async Task<Result> ExecutarAsync(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        await vincularVoluntarioMinisterioValidator.CheckAsync(command);

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
        
        await Uow.BeginAsync();
        var result = voluntario.VincularMinisterio(ministerioId);
        
        if (result.IsFailure)
        {
            await Uow.RollbackAsync();
            return result.Error;
        }
        await Uow.CommitAsync(cancellation);
      
        return Result.Success();
    }
}
