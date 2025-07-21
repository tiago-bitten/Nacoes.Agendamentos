using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using VoluntarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.Voluntario>;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

public sealed class AdicionarVoluntarioHandler(IUnitOfWork uow,
                                               IValidator<AdicionarVoluntarioCommand> commandValidator,
                                               IVoluntarioRepository voluntarioRepository)
    : ICommandHandler<AdicionarVoluntarioCommand, VoluntarioId>
{
    public async Task<Result<VoluntarioId>> Handle(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        var commandResult = await commandValidator.CheckAsync(command, cancellation);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }
        
        var existeVoluntarioComEmail = await voluntarioRepository.RecuperarPorEmailAddressAsync(command.Email ?? string.Empty);
        if (existeVoluntarioComEmail is not null)
        {
            // TODO: Add warning, voluntario pode ter email duplicado, pois pode ser dependente
        }

        var voluntarioResult = command.ToDomain();
        if (voluntarioResult.IsFailure)
        {
            return voluntarioResult.Error;
        }
        
        var voluntario = voluntarioResult.Value;
        await uow.BeginAsync();
        await voluntarioRepository.AddAsync(voluntario);
        await uow.CommitAsync(cancellation);
        
        return Result<VoluntarioId>.Success(voluntario.Id);
    }
}
