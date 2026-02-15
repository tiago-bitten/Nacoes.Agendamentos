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
    public async Task<Result<LoginResponse>> HandleAsync(
        LoginCommand command,
        CancellationToken ct)
    {
        var strategy = authStrategyFactory.Create(command.AuthType);

        var userResult = await strategy.AuthenticateAsync(command, ct);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }

        var user = userResult.Value;

        var authToken = tokenGenerator.GenerateAuth(user);
        var refreshToken = tokenGenerator.GenerateRefresh();

        return new LoginResponse(authToken, refreshToken);
    }
}
