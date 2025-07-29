namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed record LoginExternoResponse
{
    public bool DeveCriarConta { get; init; }
}