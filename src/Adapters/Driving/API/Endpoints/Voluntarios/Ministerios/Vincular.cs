using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Voluntarios.Commands.Vincular;

namespace API.Endpoints.Voluntarios.Ministerios;

internal sealed class Vincular : IEndpoint
{
    public sealed record Request(Guid MinistryId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("v1/volunteers/{volunteerId:guid}/ministries", async (
            [FromRoute] Guid volunteerId,
            [FromBody] Request request,
            [FromServices] ICommandHandler<LinkVolunteerMinistryCommand> handler,
            CancellationToken ct) =>
        {
            var command = new LinkVolunteerMinistryCommand(volunteerId, request.MinistryId);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Volunteers);
    }
}
