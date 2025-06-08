using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;
public sealed class VincularVoluntarioMinisterioHandler(IUnitOfWork uow,
                                                        IValidator<VincularVoluntarioMinisterioCommand> vincularVoluntarioMinisterioValidator,
                                                        IVoluntarioRepository voluntarioRepository,
                                                        IMinisterioRepository ministerioRepository)
    : BaseHandler(uow), IVincularVoluntarioMinisterioHandler
{
    public async Task<Result<Id<VoluntarioMinisterio>, Error>> ExecutarAsync(VincularVoluntarioMinisterioCommand command, CancellationToken cancellation = default)
    {
        await vincularVoluntarioMinisterioValidator.CheckAsync(command);

        var voluntario = await voluntarioRepository.GetByIdAsync(command.VoluntarioId, includes: "Ministerios")
                                                   .OrElse(ExceptionFactory.VoluntarioNaoEncontrado);

        Id<Ministerio> ministerioId = await ministerioRepository.GetByIdToProject(command.MinisterioId)
                                                                .Select(x => x.Id)
                                                                .FirstOrDefaultAsync(cancellation)
                                                                .OrElse(ExceptionFactory.MinisterioNaoEncontrado);
        await Uow.BeginAsync();
        voluntario.VincularMinisterio(ministerioId);
        await Uow.CommitAsync(cancellation);
      
        return voluntario.Ministerios.Last().Id;
    }
}
