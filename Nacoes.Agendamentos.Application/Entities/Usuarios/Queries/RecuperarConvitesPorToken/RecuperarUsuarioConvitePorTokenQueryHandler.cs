using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

internal sealed class RecuperarUsuarioConvitePorTokenQueryHandler(INacoesDbContext context)
    : IQueryHandler<RecuperarUsuarioConvitePorTokenQuery, RecuperarUsuarioConvitePorTokenResponse>
{
    public async Task<Result<RecuperarUsuarioConvitePorTokenResponse>> Handle(
        RecuperarUsuarioConvitePorTokenQuery query,
        CancellationToken cancellationToken = default)
    {
        var usuarioConviteResponse = await context.Convites
            .WhereSpec(new ConvitesPorTokenSpec(query.Token))
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