using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;
public record RecuperarVoluntarioResponse : BaseQueryListResponse
{
    public List<Item> Items { get; set; } = [];

    public record Item
    {
        public required string Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public required string Nome { get; set; }
        public string? Email { get; set; }
        public List<MinisterioItem> Ministerios { get; set; } = [];
    }

    public record MinisterioItem
    {
        public string Nome { get; set; } = string.Empty;
    }
}
