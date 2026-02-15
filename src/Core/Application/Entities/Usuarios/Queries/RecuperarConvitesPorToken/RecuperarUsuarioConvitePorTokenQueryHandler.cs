using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.Specs;

namespace Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

internal sealed class GetUserInvitationByTokenQueryHandler(INacoesDbContext context)
    : IQueryHandler<GetUserInvitationByTokenQuery, GetUserInvitationByTokenResponse>
{
    public async Task<Result<GetUserInvitationByTokenResponse>> Handle(
        GetUserInvitationByTokenQuery query,
        CancellationToken ct)
    {
        var invitationResponse = await context.Invitations
            .AsNoTracking()
            .ApplySpec(new InvitationsByTokenSpec(query.Token))
            .Select(x => new GetUserInvitationByTokenResponse
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email.Address,
                SentByName = x.SentBy.Name,
                Status = x.Status
            })
            .SingleOrDefaultAsync(ct);

        if (invitationResponse is null)
        {
            return UserInvitationErrors.InvitationNotFound;
        }

        return invitationResponse;
    }
}
