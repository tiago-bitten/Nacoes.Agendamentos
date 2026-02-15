using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Infra;
using Application.Shared.Messaging;
using Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

namespace API.Endpoints.Usuarios.Convites;

internal sealed class RecuperarPorToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/user-invitations/token/{token}", async (
            [FromRoute] string token,
            [FromServices] IQueryHandler<
                GetUserInvitationByTokenQuery,
                GetUserInvitationByTokenResponse> handler,
            CancellationToken ct) =>
        {
            var query = new GetUserInvitationByTokenQuery(token);

            var result = await handler.Handle(query, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Invitations);
    }
}
