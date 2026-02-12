using Application.Shared.Messaging;
using Domain.Usuarios;

namespace Application.Authentication.Commands.Login;

public record LoginCommand(
    string? Email,
    string? Senha,
    string? TokenExterno,
    EAuthType AuthType) : ICommand<LoginResponse>;
