using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Authentication.Commands.LoginExterno;

namespace API.Endpoints.Auth;

internal sealed class LoginExterno : IEndpoint
{
    public sealed record Request(string Cpf, DateOnly BirthDate);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/auth/external-login", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<ExternalLoginCommand> handler,
            CancellationToken ct) =>
        {
            var command = new ExternalLoginCommand(request.Cpf, request.BirthDate);
            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Auth);
    }
}
