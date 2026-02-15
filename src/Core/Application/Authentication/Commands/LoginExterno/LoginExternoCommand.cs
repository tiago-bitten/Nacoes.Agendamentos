using Application.Shared.Messaging;

namespace Application.Authentication.Commands.LoginExterno;

public sealed record ExternalLoginCommand(string Cpf, DateOnly BirthDate) : ICommand;
