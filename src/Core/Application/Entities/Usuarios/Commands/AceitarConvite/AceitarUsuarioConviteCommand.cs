using Application.Shared.Messaging;
using Application.Authentication.Commands.Login;
using Application.Common.Dtos;
using Application.Entities.Usuarios.Commands.Adicionar;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

public sealed record AcceptUserInvitationCommand(
    Guid UserInvitationId,
    string? ExternalToken,
    string? Password,
    EAuthType AuthType,
    PhoneNumberItemDto? PhoneNumber) : ICommand<AcceptUserInvitationResponse>;

public static class AcceptUserInvitationCommandExtensions
{
    public static AddUserCommand ToAddUserCommand(
        this AcceptUserInvitationCommand command,
        string name,
        string email,
        List<Guid> ministryIds) => new()
        {
            Name = name,
            Email = email,
            AuthType = command.AuthType,
            PhoneNumber = command.PhoneNumber is null
                ? null
                : new AddUserCommand.PhoneNumberItem
                {
                    AreaCode = command.PhoneNumber.AreaCode,
                    Number = command.PhoneNumber.Number
                },
            Password = command.Password,
            MinistryIds = ministryIds
        };

    public static LoginCommand ToLoginCommand(this AcceptUserInvitationCommand command, string email)
        => new(email, command.Password, command.ExternalToken, command.AuthType);
}
