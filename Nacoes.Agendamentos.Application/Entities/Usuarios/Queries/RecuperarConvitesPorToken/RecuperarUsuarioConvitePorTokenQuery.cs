using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

public sealed record RecuperarUsuarioConvitePorTokenQuery(string Token)
    : IQuery<RecuperarUsuarioConvitePorTokenResponse>;
