using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;

public record RecuperarVoluntarioParam : BaseQueryListParam
{
    public string? Nome { get; set; }
    public List<string> MinisteriosIds { get; set; } = [];
}
