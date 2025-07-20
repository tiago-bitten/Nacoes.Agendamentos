using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

public record AdicionarUsuarioCommand : ICommand<Guid>
{
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required EAuthType AuthType { get; init; }
    public string? Senha { get; init; }
    public CelularItem? Celular { get; init; }
    
    public record CelularItem
    {
        public required string Ddd { get; init; }
        public required string Numero { get; init; }
    }
}