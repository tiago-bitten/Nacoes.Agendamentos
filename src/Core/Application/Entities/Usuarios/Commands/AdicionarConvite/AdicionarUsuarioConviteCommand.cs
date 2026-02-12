using Application.Shared.Messaging;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

public sealed record AdicionarUsuarioConviteCommand(
    string Nome,
    string EmailAddress,
    List<Guid> MinisteriosIds) : ICommand<UsuarioConviteResponse>;
