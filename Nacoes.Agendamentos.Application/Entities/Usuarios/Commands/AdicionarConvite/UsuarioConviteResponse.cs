namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed record UsuarioConviteResponse
{
    public required string Link { get; init; }
}