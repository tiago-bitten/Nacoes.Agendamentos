using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.API.Endpoints.Usuarios.Convites;

internal sealed class Aceitar : IEndpoint
{
    public sealed record Request(string? TokenExterno, string? Senha, EAuthType AuthType, CelularItemDto Celular);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/usuarios-convites/{id:guid}/aceitar", async (
            Guid id,
            Request request,
            ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AceitarUsuarioConviteCommand(
                id,
                request.TokenExterno,
                request.Senha,
                request.AuthType,
                request.Celular);
            
            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}