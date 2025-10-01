using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

namespace Nacoes.Agendamentos.API.Endpoints.Ministerios;

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