using Application.Shared.Messaging;

namespace Application.Entities.Usuarios.Commands.RecusarConvite;

public sealed record DeclineUserInvitationCommand(Guid UserInvitationId) : ICommand;
