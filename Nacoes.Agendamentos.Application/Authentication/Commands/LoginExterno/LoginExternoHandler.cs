using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specs;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed class LoginExternoHandler(INacoesDbContext context,
                                        IAmbienteContext ambienteContext)
    : ICommandHandler<LoginExternoCommand>
{
    public async Task<Result> HandleAsync(LoginExternoCommand command, CancellationToken cancellationToken = default)
    {
        var voluntario = await context.Voluntarios
            .ApplySpec(new VoluntarioParaLoginExternoSpec(
                new DataNascimento(command.DataNascimento),
                new CPF(command.Cpf)))
            .Select(x => new 
            { 
                x.Id, 
                x.EmailAddress
            }).SingleOrDefaultAsync(cancellationToken);
        if (voluntario is null)
        {
            return VoluntarioErrors.DeveCriarConta;
        }
        
        ambienteContext.StartThirdPartyUserSession(voluntario.Id, voluntario.EmailAddress);
        
        return Result.Success();
    }
}
