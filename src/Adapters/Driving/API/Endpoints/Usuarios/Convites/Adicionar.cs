using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Usuarios.Commands.AdicionarConvite;

namespace API.Endpoints.Usuarios.Convites;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Nome, string EmailAddress, List<Guid> MinisteriosIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/usuarios/convites", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AdicionarUsuarioConviteCommand, UsuarioConviteResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AdicionarUsuarioConviteCommand(request.Nome, request.EmailAddress, request.MinisteriosIds);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Convites);
    }
}
