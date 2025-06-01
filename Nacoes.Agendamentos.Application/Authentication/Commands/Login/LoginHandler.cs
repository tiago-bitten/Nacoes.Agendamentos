using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Authentication.Validators;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public sealed class LoginHandler(IUnitOfWork uow,
                                 IValidator<LoginCommand> loginValidtor)
    : BaseHandler(uow), ILoginHandler
{
    public async Task<Result<LoginResponse, Error>> ExecutarAsync(LoginCommand command, CancellationToken cancellationToken)
    { 
        await loginValidtor.CheckAsync(command);

        throw new NotImplementedException();
    }
}
