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

internal sealed class ExternalLoginHandler(
    INacoesDbContext context,
    IEnvironmentContext environmentContext)
    : ICommandHandler<ExternalLoginCommand>
{
    public async Task<Result> HandleAsync(
        ExternalLoginCommand command,
        CancellationToken ct)
    {
        var volunteer = await context.Volunteers
            .ApplySpec(new VolunteerForExternalLoginSpec(
                new BirthDate(command.BirthDate),
                new CPF(command.Cpf)))
            .Select(x => new
            {
                x.Id,
                x.EmailAddress
            }).SingleOrDefaultAsync(ct);
        if (volunteer is null)
        {
            return VolunteerErrors.MustCreateAccount;
        }

        environmentContext.StartExternalSession(volunteer.Id, volunteer.EmailAddress);

        return Result.Success();
    }
}
