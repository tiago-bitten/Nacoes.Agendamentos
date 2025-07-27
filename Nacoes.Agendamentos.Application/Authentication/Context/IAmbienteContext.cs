﻿using Nacoes.Agendamentos.Application.Common.Enums;

namespace Nacoes.Agendamentos.Application.Authentication.Context;

public interface IAmbienteContext
{
    Guid UserId { get; }
    bool IsUsuarioAuthenticated { get; }
    bool IsUsuario { get; }
    bool IsBot { get; }
    bool IsThirdPartyUser { get; }
    void StartBotSession();
    void StartThirdPartyUserSession(Guid id, string? email);
    EEnvironment GetEnvironment();
}