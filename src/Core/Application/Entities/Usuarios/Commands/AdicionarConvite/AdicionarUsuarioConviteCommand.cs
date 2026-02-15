using Application.Shared.Messaging;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed record AddUserInvitationCommand(
    string Name,
    string EmailAddress,
    List<Guid> MinistryIds) : ICommand<UserInvitationResponse>;
