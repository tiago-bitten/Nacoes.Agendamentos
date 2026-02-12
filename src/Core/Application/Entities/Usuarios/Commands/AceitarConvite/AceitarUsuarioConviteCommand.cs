using Application.Shared.Messaging;
using Application.Authentication.Commands.Login;
using Application.Common.Dtos;
using Application.Entities.Usuarios.Commands.Adicionar;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

public sealed record AceitarUsuarioConviteCommand(
    Guid UsuarioConviteId,
    string? TokenExterno,
    string? Senha,
    EAuthType AuthType,
    CelularItemDto? Celular) : ICommand<AceitarUsuarioConviteResponse>;

public static class AceitarUsuarioConviteCommandExtensions
{
    public static AdicionarUsuarioCommand ToAdicionarUsuarioCommand(
        this AceitarUsuarioConviteCommand command,
        string nome,
        string email,
        List<Guid> ministeriosIds) => new()
        {
            Nome = nome,
            Email = email,
            AuthType = command.AuthType,
            Celular = command.Celular is null
                ? null
                : new AdicionarUsuarioCommand.CelularItem
                {
                    Ddd = command.Celular.Ddd,
                    Numero = command.Celular.Numero
                },
            Senha = command.Senha,
            MinisteriosIds = ministeriosIds
        };

    public static LoginCommand ToLoginCommand(this AceitarUsuarioConviteCommand command, string email)
        => new(email, command.Senha, command.TokenExterno, command.AuthType);
}
