using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

namespace Nacoes.Agendamentos.API.Endpoints.Auth;

internal sealed class LoginExterno : IEndpoint
{
    public sealed record Request(string Cpf, DateOnly DataNascimento);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/v1/login-externo", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<LoginExternoCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginExternoCommand(request.Cpf, request.DataNascimento);
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        });
    }
}