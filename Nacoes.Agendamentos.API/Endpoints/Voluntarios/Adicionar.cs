using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.API.Endpoints.Voluntarios;

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