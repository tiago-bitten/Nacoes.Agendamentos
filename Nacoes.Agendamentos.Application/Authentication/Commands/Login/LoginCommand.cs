using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public record LoginCommand
{
    public string? Email { get; init; }
    public string? Senha { get; init; }
    public string? TokenExterno { get; init; }
    public required EAuthType AuthType { get; init; }
}
