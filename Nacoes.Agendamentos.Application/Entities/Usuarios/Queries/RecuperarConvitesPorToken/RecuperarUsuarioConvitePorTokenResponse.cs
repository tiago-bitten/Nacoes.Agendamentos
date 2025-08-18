using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

public sealed record RecuperarUsuarioConvitePorTokenResponse
{
    public required Guid Id { get; init; }
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required string NomeEnviadoPor { get; init; }
    public required EConviteStatus Status { get; init; }
}