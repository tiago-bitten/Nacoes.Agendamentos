using Domain.Usuarios;

namespace Application.Entities.Usuarios.Queries.RecuperarConvitesPorToken;

public sealed record GetUserInvitationByTokenResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string SentByName { get; init; }
    public required EInvitationStatus Status { get; init; }
}
