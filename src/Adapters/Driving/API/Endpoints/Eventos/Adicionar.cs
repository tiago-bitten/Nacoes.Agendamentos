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
        string Description,
        ScheduleDto Schedule,
        EventRecurrenceDto Recurrence,
        int? MaxReservationCount);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/events", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AddEventCommand, Guid> handler,
            CancellationToken ct) =>
        {
            var command = new AddEventCommand(
                request.Description,
                request.Schedule,
                request.Recurrence,
                request.MaxReservationCount);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Events);
    }
}
