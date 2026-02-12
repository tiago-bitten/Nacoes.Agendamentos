using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Common.Dtos;
using Application.Entities.Usuarios.Commands.AceitarConvite;
using Domain.Usuarios;

namespace API.Endpoints.Usuarios.Convites;

internal sealed class Aceitar : IEndpoint
{
    public sealed record Request(string? TokenExterno, string? Senha, EAuthType AuthType, CelularItemDto Celular);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/usuarios-convites/{id:guid}/aceitar", async (
            [FromRoute] Guid id,
            [FromBody] Request request,
            [FromServices] ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse> handler,
            [FromServices] IAmbienteContext ambienteContext,
            CancellationToken cancellationToken) =>
        {
            ambienteContext.StartBotSession();

            var command = new AceitarUsuarioConviteCommand(
                id,
                request.TokenExterno,
                request.Senha,
                request.AuthType,
                request.Celular);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Convites);
    }
}
