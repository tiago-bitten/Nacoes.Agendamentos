using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Authentication.Commands.Login;
using Domain.Usuarios;

namespace API.Endpoints.Auth;

internal sealed class Login : IEndpoint
{
    public record Request(
        string? Email,
        string? Senha,
        string? TokenExterno,
        EAuthType AuthType);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/login", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<LoginCommand, LoginResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginCommand(request.Email, request.Senha, request.TokenExterno, request.AuthType);
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Auth);
    }
}
