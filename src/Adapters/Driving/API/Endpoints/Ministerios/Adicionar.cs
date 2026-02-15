using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Common.Dtos;
using Application.Entities.Ministerios.Commands.Adicionar;

namespace API.Endpoints.Ministerios;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Name, string? Description, ColorDto Color);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/ministries", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AddMinistryCommand, Guid> handler,
            CancellationToken ct) =>
        {
            var command = new AddMinistryCommand(request.Name, request.Description, request.Color);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministries);
    }
}
