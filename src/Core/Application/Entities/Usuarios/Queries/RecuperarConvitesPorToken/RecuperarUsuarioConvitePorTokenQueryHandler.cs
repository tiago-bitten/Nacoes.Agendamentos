using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.Specs;

namespace Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

internal sealed class RecuperarUsuarioConvitePorTokenQueryHandler(INacoesDbContext context)
    : IQueryHandler<RecuperarUsuarioConvitePorTokenQuery, RecuperarUsuarioConvitePorTokenResponse>
{
    public async Task<Result<RecuperarUsuarioConvitePorTokenResponse>> Handle(
        RecuperarUsuarioConvitePorTokenQuery query,
        CancellationToken cancellationToken = default)
    {
        var usuarioConviteResponse = await context.Convites
            .ApplySpec(new ConvitesPorTokenSpec(query.Token))
            .Select(x => new RecuperarUsuarioConvitePorTokenResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email.Address,
                NomeEnviadoPor = x.EnviadoPor.Nome,
                Status = x.Status
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (usuarioConviteResponse is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }

        return usuarioConviteResponse;
    }
}
