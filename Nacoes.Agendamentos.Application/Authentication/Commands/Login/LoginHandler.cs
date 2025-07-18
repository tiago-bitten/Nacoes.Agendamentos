using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public sealed class LoginHandler(IUnitOfWork uow,
                                 IValidator<LoginCommand> loginValidator,
                                 IAuthStrategyFactory authStrategyFactory,
                                 ITokenGenerator tokenGenerator)
    : BaseHandler(uow), ILoginHandler
{
    public async Task<Result<LoginResponse>> ExecutarAsync(LoginCommand command, CancellationToken cancellationToken)
    { 
        await loginValidator.CheckAsync(command);

        var strategy = authStrategyFactory.Criar(command.AuthType);
        
        var result = await strategy.AutenticarAsync(command);
        if (result.IsFailure)
        {
            return result.Error;
        }
        
        var usuario = result.Value;
        
        var authToken = tokenGenerator.GenerateAuth(usuario);
        var refreshToken = tokenGenerator.GenerateRefresh();

        var loginResponse = new LoginResponse
        {
            AuthToken = authToken,
            RefreshToken = refreshToken
        };

        return Result<LoginResponse>.Success(loginResponse);
    }
}
