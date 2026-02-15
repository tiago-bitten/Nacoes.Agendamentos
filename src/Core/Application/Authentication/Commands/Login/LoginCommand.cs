using Application.Shared.Messaging;
using Domain.Usuarios;

namespace Application.Authentication.Commands.Login;

public sealed record LoginCommand(
    string? Email,
    string? Password,
    string? ExternalToken,
    EAuthType AuthType) : ICommand<LoginResponse>;
