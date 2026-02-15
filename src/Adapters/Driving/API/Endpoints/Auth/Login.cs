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
        string? Password,
        string? ExternalToken,
        EAuthType AuthType);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/auth/login", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<LoginCommand, LoginResponse> handler,
            CancellationToken ct) =>
        {
            var command = new LoginCommand(request.Email, request.Password, request.ExternalToken, request.AuthType);
            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Auth);
    }
}
