using Application.Shared.Messaging;
using Application.Entities.Usuarios.Commands.AdicionarConvite;

namespace Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

public sealed record GetUserInvitationByTokenQuery(string Token)
    : IQuery<GetUserInvitationByTokenResponse>;
