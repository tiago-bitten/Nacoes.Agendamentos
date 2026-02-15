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
    public sealed record Request(string? ExternalToken, string? Password, EAuthType AuthType, PhoneNumberItemDto PhoneNumber);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("v1/user-invitations/{id:guid}/accept", async (
            [FromRoute] Guid id,
            [FromBody] Request request,
            [FromServices] ICommandHandler<AcceptUserInvitationCommand, AcceptUserInvitationResponse> handler,
            [FromServices] IEnvironmentContext environmentContext,
            CancellationToken ct) =>
        {
            environmentContext.StartBotSession();

            var command = new AcceptUserInvitationCommand(
                id,
                request.ExternalToken,
                request.Password,
                request.AuthType,
                request.PhoneNumber);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Invitations);
    }
}
