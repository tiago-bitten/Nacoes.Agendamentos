using Nacoes.Agendamentos.ReadModels.Abstracts;

namespace Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;

public record RecuperarUsuarioParams : BaseQueryListParam
{
    public string? Nome { get; init; }
}
