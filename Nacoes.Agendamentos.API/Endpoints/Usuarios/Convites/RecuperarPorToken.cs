using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

namespace Nacoes.Agendamentos.API.Endpoints.Usuarios.Convites;

internal sealed class RecuperarPorToken : IEndpoint 
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/usuarios-convites/token/{token}", async (
            string token,
            IQueryHandler<RecuperarUsuarioConvitePorTokenQuery, RecuperarUsuarioConvitePorTokenResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new RecuperarUsuarioConvitePorTokenQuery(token);
            
            var result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}