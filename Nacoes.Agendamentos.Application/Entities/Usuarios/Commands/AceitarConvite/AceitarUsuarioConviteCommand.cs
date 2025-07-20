using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

public record AceitarUsuarioConviteCommand : ICommand<AceitarUsuarioConviteResponse>
{
    public Guid UsuarioConviteId { get; init; }
    public string? TokenExterno { get; init; }
    public string? Senha { get; init; }
    public EAuthType AuthType { get; init; }
    public CelularItem? Celular { get; init; }
    
    public record CelularItem
    {
        public string Ddd { get; init; } = string.Empty;
        public string Numero { get; init; } = string.Empty;
    }
}

public static class AceitarUsuarioConviteCommandExtensions
{
    public static AdicionarUsuarioCommand ToAdicionarUsuarioCommand(
        this AceitarUsuarioConviteCommand command, string nome, string email)
        => new()
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
            Senha = command.Senha
        };

    public static LoginCommand ToLoginCommand(this AceitarUsuarioConviteCommand command, string email)
        => new()
        {
            Email = email,
            Senha = command.Senha,
            AuthType = command.AuthType,
            TokenExterno = command.TokenExterno
        };
}