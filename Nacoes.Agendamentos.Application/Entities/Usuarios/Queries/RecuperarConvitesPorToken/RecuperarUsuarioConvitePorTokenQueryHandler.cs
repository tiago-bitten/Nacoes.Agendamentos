using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

internal sealed class RecuperarUsuarioConvitePorTokenQueryHandler(IUsuarioConviteRepository usuarioConviteRepository)
    : IQueryHandler<RecuperarUsuarioConvitePorTokenQuery, RecuperarUsuarioConvitePorTokenResponse>
{
    public async Task<Result<RecuperarUsuarioConvitePorTokenResponse>> Handle(RecuperarUsuarioConvitePorTokenQuery query, CancellationToken cancellationToken = default)
    {
        var usuarioConviteResponse = await usuarioConviteRepository
            .RecuperarPorToken(query.Token)
            .Include(x => x.EnviadoPor)
            .Select(x => new RecuperarUsuarioConvitePorTokenResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email.Address,
                NomeEnviadoPor = x.EnviadoPor.Nome,
                Status = x.Status
            })
            .AsSplitQuery()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);
        
        if (usuarioConviteResponse is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }

        return Result<RecuperarUsuarioConvitePorTokenResponse>.Success(usuarioConviteResponse);
    }
}