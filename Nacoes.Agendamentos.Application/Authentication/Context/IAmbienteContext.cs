namespace Nacoes.Agendamentos.Application.Authentication.Context;

public interface IAmbienteContext
{
    Guid UsuarioId { get; }
    bool IsUsuarioAuthenticated { get; }
    bool IsBot { get; }
    void StartBotSession();
}