using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed class LoginExternoHandler(IVoluntarioRepository voluntarioRepository,
                                        IAmbienteContext ambienteContext)
    : ICommandHandler<LoginExternoCommand>
{
    public async Task<Result> HandleAsync(LoginExternoCommand command, CancellationToken cancellationToken = default)
    {
        var voluntario = await voluntarioRepository.RecuperarParaLoginExterno(command.DataNascimento, command.Cpf)
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
