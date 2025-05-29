namespace Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;

public record AdicionarUsuarioCommand
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Senha { get; set; }
    public IList<Ministerio> Ministerios { get; set; } = [];

    public record Ministerio
    {
        public string Id { get; set; } = string.Empty;
    }
}