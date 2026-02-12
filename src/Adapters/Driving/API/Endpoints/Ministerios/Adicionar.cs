using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Common.Dtos;
using Application.Entities.Ministerios.Commands.Adicionar;

namespace API.Endpoints.Ministerios;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Nome, string? Descricao, CorDto Cor);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/ministerios", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarMinisterioCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarMinisterioCommand(request.Nome, request.Descricao, request.Cor);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Ministerios);
    }
}
