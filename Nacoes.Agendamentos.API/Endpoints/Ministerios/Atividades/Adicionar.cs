using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

namespace Nacoes.Agendamentos.API.Endpoints.Ministerios.Atividades;

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
        });
    }
}