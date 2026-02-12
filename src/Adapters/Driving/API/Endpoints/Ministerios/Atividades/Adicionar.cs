using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Ministerios.Commands.AdicionarAtividade;

namespace API.Endpoints.Ministerios.Atividades;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Nome, string? Descricao);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/ministerios/{ministerioId:guid}/atividades", async (
            [FromRoute] Guid ministerioId,
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarAtividadeCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarAtividadeCommand(request.Nome, request.Descricao, ministerioId);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Atividades);
    }
}
