using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

namespace Nacoes.Agendamentos.API.Endpoints.Eventos;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(
        string Descricao,
        HorarioDto Horario,
        RecorrenciaEventoDto Recorrencia);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/eventos", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarEventoCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarEventoCommand(request.Descricao, request.Horario, request.Recorrencia);
            
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Eventos);
    }
}
