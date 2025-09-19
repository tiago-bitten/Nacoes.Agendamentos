using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

internal sealed class AdicionarVoluntarioHandler(
    INacoesDbContext context, 
    IHistoricoRegister historicoRegister)
    : ICommandHandler<AdicionarVoluntarioCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        if (!string.IsNullOrEmpty(command.Email))
        {
            var existeVountarioComEmail = await context.Voluntarios
                .ApplySpec(new VoluntarioComEmailAddressSpec(command.Email))
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
        await context.Voluntarios.AddAsync(voluntario, cancellation);
        await context.SaveChangesAsync(cancellation);

        // TODO: move para domain event
        await historicoRegister.AuditAsync(voluntario, acao: "Voluntário adicionado.");
        
        voluntario.Raise(new VoluntarioAdicionadoDomainEvent(voluntario.Id));
        
        return voluntario.Id;
    }
}
