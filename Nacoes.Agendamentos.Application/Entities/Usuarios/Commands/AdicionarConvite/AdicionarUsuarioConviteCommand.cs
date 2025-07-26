using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed record AdicionarUsuarioConviteCommand : ICommand<UsuarioConviteResponse>
{
    public required string Nome { get; init; }
    public required string Email { get; init; }
}