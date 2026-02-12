using Application.Shared.Messaging;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.Adicionar;

public sealed record AdicionarUsuarioCommand : ICommand<Guid>
{
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required EAuthType AuthType { get; init; }
    public List<Guid> MinisteriosIds { get; init; } = [];
    public string? Senha { get; init; }
    public CelularItem? Celular { get; init; }

    public record CelularItem
    {
        public required string Ddd { get; init; }
        public required string Numero { get; init; }
    }
}
