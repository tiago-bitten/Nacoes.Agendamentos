using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public sealed class LoginHandler(IValidator<LoginCommand> loginValidator,
                                 IAuthStrategyFactory authStrategyFactory,
                                 ITokenGenerator tokenGenerator)
    : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    { 
        var commandResult = await loginValidator.CheckAsync(command, cancellationToken);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }
        
        var strategy = authStrategyFactory.Criar(command.AuthType);
        
        var usuarioResult = await strategy.AutenticarAsync(command);
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }
        
        var usuario = usuarioResult.Value;
        
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
