namespace Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;
public interface IRecuperarUsuarioQuery
{
    Task<RecuperarUsuarioResponse> ExecutarAsync(RecuperarUsuarioParams @params);
}
