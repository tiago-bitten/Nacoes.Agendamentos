using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed record LoginExternoCommand : ICommand
{
    public required string Cpf { get; init; }
    public required DateOnly DataNascimento { get; init; }
}
