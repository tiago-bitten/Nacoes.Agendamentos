using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public sealed record EnviarUsuarioConviteCommand : ICommand, ICommand<string>
{
    public required string Nome { get; init; }
    public required string Email { get; init; }
}