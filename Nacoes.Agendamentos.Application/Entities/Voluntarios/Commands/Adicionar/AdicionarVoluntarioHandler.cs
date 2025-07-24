﻿using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using VoluntarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.Voluntario>;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class AdicionarVoluntarioHandler(IUnitOfWork uow,
                                                IVoluntarioRepository voluntarioRepository,
                                                IHistoricoRegister historico)
    : ICommandHandler<AdicionarVoluntarioCommand, VoluntarioId>
{
    public async Task<Result<VoluntarioId>> Handle(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        // TODO: move para domain event
        if (!string.IsNullOrEmpty(command.Email))
        {
            var existeVountarioComEmail = await voluntarioRepository.RecuperarPorEmailAddress(command.Email)
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
        await uow.BeginAsync();
        await voluntarioRepository.AddAsync(voluntario);
        await uow.CommitAsync(cancellation);

        // TODO: move para domain event
        await historico.AcaoCriarAsync(voluntario);
        
        voluntario.Raise(new VoluntarioAdicionadoDomainEvent(voluntario.Id));
        
        return Result<VoluntarioId>.Success(voluntario.Id);
    }
}
