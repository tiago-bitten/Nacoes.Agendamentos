namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public record EnviarUsuarioConviteResponse
{
    public required string LinkConvite { get; init; }
}