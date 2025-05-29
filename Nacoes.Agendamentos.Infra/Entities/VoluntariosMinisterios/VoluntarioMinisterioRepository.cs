using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.VoluntariosMinisterios;

public class VoluntarioMinisterioRepository(NacoesDbContext dbContext) : BaseRepository<VoluntarioMinisterio>(dbContext), IVoluntarioMinisterioRepository
{
}
