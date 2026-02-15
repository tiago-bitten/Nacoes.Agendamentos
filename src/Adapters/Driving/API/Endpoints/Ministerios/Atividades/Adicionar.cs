using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Ministerios.Commands.AdicionarAtividade;

namespace API.Endpoints.Ministerios.Atividades;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Name, string? Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/ministries/{ministryId:guid}/activities", async (
            [FromRoute] Guid ministryId,
            [FromBody] Request request,
            [FromServices] ICommandHandler<AddActivityCommand, Guid> handler,
            CancellationToken ct) =>
        {
            var command = new AddActivityCommand(request.Name, request.Description, ministryId);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Activities);
    }
}
