using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Usuarios.Commands.RecusarConvite;

namespace API.Endpoints.Usuarios.Convites;

internal sealed class Recusar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("v1/user-invitations/{id:guid}/decline", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<DeclineUserInvitationCommand> handler,
            CancellationToken ct) =>
        {
            var command = new DeclineUserInvitationCommand(id);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Invitations);
    }
}
