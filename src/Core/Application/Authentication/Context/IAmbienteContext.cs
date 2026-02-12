using Application.Common.Enums;
using Domain.Enums;

namespace Application.Authentication.Context;

public interface IAmbienteContext
{
    Guid UserId { get; }
    bool IsUserAuthenticated { get; }
    EUserContextType UserContextType { get; }
    void StartBotSession();
    void StartExternalSession(Guid id, string? email);
    EEnvironment Environment { get; }
}
