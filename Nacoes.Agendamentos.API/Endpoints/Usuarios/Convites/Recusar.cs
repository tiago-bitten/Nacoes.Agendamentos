using Microsoft.AspNetCore.Mvc;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

namespace Nacoes.Agendamentos.API.Endpoints.Usuarios.Convites;

internal sealed class Recusar : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/usuarios-convites/{id:guid}/recusar", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<RecusarUsuarioConviteCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RecusarUsuarioConviteCommand(id);
            
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        });
    }
}