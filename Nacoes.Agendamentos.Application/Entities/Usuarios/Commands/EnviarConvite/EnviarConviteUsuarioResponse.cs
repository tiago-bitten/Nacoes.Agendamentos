namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public record EnviarConviteUsuarioResponse
{
    public required string LinkConvite { get; init; }
}