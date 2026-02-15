namespace Application.Entities.Usuarios.Commands.AceitarConvite;

public sealed record AcceptUserInvitationResponse(string AuthToken, string RefreshToken);
