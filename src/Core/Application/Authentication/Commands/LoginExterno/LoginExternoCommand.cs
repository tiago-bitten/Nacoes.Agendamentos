using Application.Shared.Messaging;

namespace Application.Authentication.Commands.LoginExterno;

public sealed record LoginExternoCommand(string Cpf, DateOnly DataNascimento) : ICommand;
