using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;
public record RecuperarUsuarioResponse : BaseQueryListResponse
{
    public IList<Item> Items { get; set; } = [];

    public record Item
    {
        public required string Id { get; init; }
        public required string Nome { get; init; }
        public required string Email { get; init; }
        public DateTime DataCriacao { get; init; }
    }
}
