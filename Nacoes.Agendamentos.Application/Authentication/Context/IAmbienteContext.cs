using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Domain.Enums;

namespace Nacoes.Agendamentos.Application.Authentication.Context;

public interface IAmbienteContext
{
    Guid UserId { get; }
    bool IsUserAuthenticated { get; }
    EUserContextType UserContextType { get; }
    void StartBotSession();
    void StartExternalSession(Guid id, string? email);
    EEnvironment Environment { get; }
}