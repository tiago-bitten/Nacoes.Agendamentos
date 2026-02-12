using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Voluntarios.Errors;
using Domain.Voluntarios.Specs;
using Domain.Shared.ValueObjects;

namespace Application.Authentication.Commands.LoginExterno;

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

        ambienteContext.StartExternalSession(voluntario.Id, voluntario.EmailAddress);

        return Result.Success();
    }
}
