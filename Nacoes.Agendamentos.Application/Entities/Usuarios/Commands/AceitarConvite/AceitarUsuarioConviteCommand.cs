using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

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