namespace Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;
public interface IRecuperarVoluntarioQuery
{
    Task<RecuperarVoluntarioResponse> ExecutarAsync(RecuperarVoluntarioParam param);
}
