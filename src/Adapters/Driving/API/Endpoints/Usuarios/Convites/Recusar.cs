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
        app.MapPut("api/v1/usuarios-convites/{id:guid}/recusar", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<RecusarUsuarioConviteCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RecusarUsuarioConviteCommand(id);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Convites);
    }
}
