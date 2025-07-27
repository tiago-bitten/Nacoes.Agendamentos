using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

public sealed record RecusarUsuarioConviteCommand(Guid UsuarioConviteId) : ICommand;