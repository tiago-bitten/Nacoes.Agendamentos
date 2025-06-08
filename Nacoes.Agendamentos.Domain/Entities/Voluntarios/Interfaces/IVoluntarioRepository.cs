using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using VoluntarioMinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.VoluntarioMinisterio>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

public interface IVoluntarioRepository : IBaseRepository<Voluntario>
{
    IQueryable<Voluntario> RecuperarPorVoluntarioMinisterio(VoluntarioMinisterioId voluntarioMinisterioId);
}
