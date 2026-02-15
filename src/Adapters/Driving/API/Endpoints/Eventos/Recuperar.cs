using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Common.Dtos;
using Domain.Shared.Results;
using Domain.Eventos;

namespace API.Endpoints.Eventos;

internal sealed class Recuperar : IEndpoint
{
    public sealed record Request(DateOnly StartDate, DateOnly EndDate);

    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Description { get; init; } = string.Empty;
        public int ReservationCount { get; init; }
        public int? MaxReservationCount { get; init; }
        public ScheduleDto Schedule { get; init; } = null!;
        public EEventStatus Status { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/events", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken ct) =>
        {
            Result<List<Response>> result;

            try
            {
                var events = await context.Events
                    .Where(x => x.Status != EEventStatus.Cancelled &&
                                DateOnly.FromDateTime(x.Schedule.StartDate.DateTime) >= request.StartDate &&
                                DateOnly.FromDateTime(x.Schedule.EndDate.DateTime) <= request.EndDate)
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Description = x.Description,
                        ReservationCount = x.ReservationCount,
                        MaxReservationCount = x.MaxReservationCount,
                        Schedule = x.Schedule.ToDto(),
                        Status = x.Status
                    })
                    .ToListAsync(ct);

                result = Result<List<Response>>.Success(events);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetEvents", ex.Message);
                result = Result<List<Response>>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Events);
    }
}
