using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

namespace Nacoes.Agendamentos.API.Endpoints.Voluntarios.Ministerios;

internal sealed class Desvincular : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/ministerios/{voluntarioMinisterioId:guid}", async (
            [FromRoute] Guid voluntarioMinisterioId,
            [FromServices] ICommandHandler<DesvincularVoluntarioMinisterioCommand> handler,
            CancellationToken cancellation) =>
        {
            var command = new DesvincularVoluntarioMinisterioCommand(voluntarioMinisterioId);
            
            var result = await handler.HandleAsync(command, cancellation);

            return result.Match(Results.NoContent, CustomResults.Problem);
        }).WithTags(Tags.Voluntarios);
    }
}