using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Common.Dtos;
using Application.Entities.Eventos.Commands.Adicionar;

namespace API.Endpoints.Eventos;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(
        string Descricao,
        HorarioDto Horario,
        RecorrenciaEventoDto Recorrencia,
        int? QuantidadeMaximaReservas);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/eventos", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarEventoCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarEventoCommand(
                request.Descricao,
                request.Horario,
                request.Recorrencia,
                request.QuantidadeMaximaReservas);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Eventos);
    }
}
