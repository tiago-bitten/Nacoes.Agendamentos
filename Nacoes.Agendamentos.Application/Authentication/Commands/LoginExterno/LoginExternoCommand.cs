using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Authentication.Commands.LoginExterno;

public sealed record LoginExternoCommand(string Cpf, DateOnly DataNascimento) : ICommand;
