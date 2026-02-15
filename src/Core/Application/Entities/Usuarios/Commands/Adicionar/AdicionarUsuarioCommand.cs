using Application.Shared.Messaging;
using Domain.Usuarios;

namespace Application.Entities.Usuarios.Commands.Adicionar;

public sealed record AddUserCommand : ICommand<Guid>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required EAuthType AuthType { get; init; }
    public List<Guid> MinistryIds { get; init; } = [];
    public string? Password { get; init; }
    public PhoneNumberItem? PhoneNumber { get; init; }

    public record PhoneNumberItem
    {
        public required string AreaCode { get; init; }
        public required string Number { get; init; }
    }
}
