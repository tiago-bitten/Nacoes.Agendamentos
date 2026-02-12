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
        app.MapDelete("api/v1/ministerios/{voluntarioMinisterioId:guid}", async (
            [FromRoute] Guid voluntarioMinisterioId,
            [FromServices] ICommandHandler<DesvincularVoluntarioMinisterioCommand> handler,
            CancellationToken cancellation) =>
        {
            var command = new DesvincularVoluntarioMinisterioCommand(voluntarioMinisterioId);

            var result = await handler.HandleAsync(command, cancellation);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Voluntarios);
    }
}
