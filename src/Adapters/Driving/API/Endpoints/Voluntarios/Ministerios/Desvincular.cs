using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Voluntarios.Commands.Desvincular;

namespace API.Endpoints.Voluntarios.Ministerios;

internal sealed class Desvincular : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("v1/ministries/{volunteerMinistryId:guid}", async (
            [FromRoute] Guid volunteerMinistryId,
            [FromServices] ICommandHandler<UnlinkVolunteerMinistryCommand> handler,
            CancellationToken ct) =>
        {
            var command = new UnlinkVolunteerMinistryCommand(volunteerMinistryId);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Volunteers);
    }
}
