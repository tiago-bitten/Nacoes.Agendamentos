using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Common.Dtos;
using Application.Entities.Voluntarios.Commands.Adicionar;
using Domain.Voluntarios;

namespace API.Endpoints.Voluntarios;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(
        string Name,
        string? Email,
        PhoneNumberItemDto? PhoneNumber,
        string? Cpf,
        DateOnly? BirthDate,
        EVolunteerRegistrationOrigin RegistrationOrigin);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/volunteers", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AddVolunteerCommand, Guid> handler,
            CancellationToken ct) =>
        {
            var command = new AddVolunteerCommand(
                request.Name,
                request.Email,
                request.PhoneNumber,
                request.Cpf,
                request.BirthDate,
                request.RegistrationOrigin);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Volunteers);
    }
}
