using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Voluntarios.Commands.Vincular;

namespace API.Endpoints.Voluntarios.Ministerios;

internal sealed class Vincular : IEndpoint
{
    public sealed record Request(Guid MinisterioId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/voluntarios/{voluntarioId:guid}/ministerios", async (
            [FromRoute] Guid voluntarioId,
            [FromBody] Request request,
            [FromServices] ICommandHandler<VincularVoluntarioMinisterioCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new VincularVoluntarioMinisterioCommand(voluntarioId, request.MinisterioId);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Voluntarios);
    }
}
