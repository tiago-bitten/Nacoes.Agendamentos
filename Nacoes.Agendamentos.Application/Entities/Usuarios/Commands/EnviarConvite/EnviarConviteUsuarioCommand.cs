namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public record EnviarConviteUsuarioCommand
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
}