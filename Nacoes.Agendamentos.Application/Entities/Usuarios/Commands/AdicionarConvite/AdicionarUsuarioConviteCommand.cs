using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed record AdicionarUsuarioConviteCommand(
    string Nome,
    string EmailAddress,
    List<Guid> MinisteriosIds) : ICommand<UsuarioConviteResponse>;
