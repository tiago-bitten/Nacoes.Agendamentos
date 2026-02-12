using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Authentication.Commands.LoginExterno;

namespace API.Endpoints.Auth;

internal sealed class LoginExterno : IEndpoint
{
    public sealed record Request(string Cpf, DateOnly DataNascimento);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/login-externo", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<LoginExternoCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginExternoCommand(request.Cpf, request.DataNascimento);
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Auth);
    }
}
