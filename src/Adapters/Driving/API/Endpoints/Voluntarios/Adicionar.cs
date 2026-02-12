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
        string Nome,
        string? Email,
        CelularItemDto? Celular,
        string? Cpf,
        DateOnly? DataNascimento,
        EOrigemCadastroVoluntario OrigemCadastro);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/voluntarios", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarVoluntarioCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarVoluntarioCommand(
                request.Nome,
                request.Email,
                request.Celular,
                request.Cpf,
                request.DataNascimento,
                request.OrigemCadastro);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Voluntarios);
    }
}
