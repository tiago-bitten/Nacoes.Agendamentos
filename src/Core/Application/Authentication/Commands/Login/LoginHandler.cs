using Application.Shared.Messaging;
using Application.Authentication.Factories;
using Application.Authentication.TokenGenerators;
using Domain.Shared.Results;

namespace Application.Authentication.Commands.Login;

internal sealed class LoginHandler(
    IAuthStrategyFactory authStrategyFactory,
    ITokenGenerator tokenGenerator)
    : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var strategy = authStrategyFactory.Criar(command.AuthType);

        var usuarioResult = await strategy.AutenticarAsync(command);
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }

        var usuario = usuarioResult.Value;

        var authToken = tokenGenerator.GenerateAuth(usuario);
        var refreshToken = tokenGenerator.GenerateRefresh();

        return new LoginResponse(authToken, refreshToken);
    }
}
