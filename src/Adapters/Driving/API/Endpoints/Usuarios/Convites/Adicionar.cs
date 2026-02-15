using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Usuarios.Commands.AdicionarConvite;

namespace API.Endpoints.Usuarios.Convites;

internal sealed class Adicionar : IEndpoint
{
    public sealed record Request(string Name, string EmailAddress, List<Guid> MinistryIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("v1/users/invitations", async (
            [FromBody] Request request,
            [FromServices] ICommandHandler<AddUserInvitationCommand, UserInvitationResponse> handler,
            CancellationToken ct) =>
        {
            var command = new AddUserInvitationCommand(request.Name, request.EmailAddress, request.MinistryIds);

            var result = await handler.HandleAsync(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Invitations);
    }
}
