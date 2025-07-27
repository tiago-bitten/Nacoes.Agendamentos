using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using UsuarioConviteId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.UsuarioConvite>;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.RecusarConvite;

public sealed record RecusarUsuarioConviteCommand(UsuarioConviteId UsuarioConviteId) : ICommand;