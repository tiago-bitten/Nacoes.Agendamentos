using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.Login;

public record LoginCommand(
    string? Email,
    string? Senha,
    string? TokenExterno,
    EAuthType AuthType) : ICommand<LoginResponse>;
