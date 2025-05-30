using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

public record AdicionarUsuarioCommand
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CelularItem Celular { get; set; } = new();
    public EAuthType AuthType { get; set; }
    public string? Senha { get; set; }
    public IList<MinisterioItem> Ministerios { get; set; } = [];

    public record CelularItem
    {
        public string Ddd { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }

    public record MinisterioItem
    {
        public string Id { get; set; } = string.Empty;
    }
}